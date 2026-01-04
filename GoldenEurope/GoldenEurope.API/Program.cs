using Asp.Versioning;
using FluentValidation;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using GoldenEurope.API.Middlewares;
using GoldenEurope.API.Models;
using GoldenEurope.Core.Interfaces;
using GoldenEurope.Business.Interfaces;
using GoldenEurope.Business.Services;
using GoldenEurope.Business.Validators;
using GoldenEurope.Persistance;
using GoldenEurope.Persistance.Repositories;

//Serilog
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    
    //connection db
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Missing connection string");
    }
    builder.Services.AddDbContext<GoldenEuropeDbContext>(options =>
        options.UseSqlServer(connectionString));
    
    //DI
    builder.Services.AddScoped<ICarRepository, CarRepository>();
    builder.Services.AddScoped<ICarService, CarService>();
    
    builder.Services.AddScoped<IBrandRepository, BrandRepository>();
    builder.Services.AddScoped<IBrandService, BrandService>();
    
    builder.Services.AddScoped<IModelRepository, ModelRepository>();
    builder.Services.AddScoped<IModelService, ModelService>();
    

    // AutoMapper 
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.ShouldMapMethod = (m => false); //fix that shit
    }, typeof(CarService).Assembly);
    
    //FluentValidation
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<CreateCarDtoValidator>();


    //Custom secure from errors(if there are error before calling controller ot wont even call it just throw an errors)
    builder.Services.Configure<ApiBehaviorOptions>(options =>
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();
            var errorMessage = string.Join("; ", errors);
            var response = ApiResponse<object>.ErrorResult(errorMessage, 400);
            return new BadRequestObjectResult(response);
        });
    
    //CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp", policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });
    
    //Rate Limit
    builder.Services.AddRateLimiter(options =>
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        options.AddFixedWindowLimiter("fixed", policy =>
        {
            policy.PermitLimit = 10;
            policy.Window = TimeSpan.FromSeconds(10);
            policy.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            policy.QueueLimit = 2;
        });
    });
    
    builder.Services.AddControllers();
    
    //API Versioning
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }).AddMvc().AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
    
    //Swagger Configuration
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Golden Europe API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT", In = ParameterLocation.Header,
            Description = "Enter your valid token in  the text input below" 
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                }, Array.Empty<string>()
            }
        });
    });

    var app = builder.Build();
    
    //Middleware
    app.UseSerilogRequestLogging();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Golden Europe API v1");
        });
    }

    app.UseMiddleware<GlobalExceptionHandler>();
    app.UseHttpsRedirection();
    app.UseCors("AllowReactApp");
    app.UseRateLimiter();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers()
        .RequireRateLimiting("fixed");
    app.Run();

}
catch (Exception e)
{
    Log.Fatal(e, "Unhandled exception");
}
finally
{
    Log.CloseAndFlush();
}