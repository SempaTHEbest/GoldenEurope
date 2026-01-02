using Asp.Versioning;
using FluentValidation;
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

// 1. Налаштування Serilog (Bootstrap Logger)
// Виправлено: тепер ми читаємо змінну середовища, щоб підтягнути правильний файл
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
    Log.Information("Starting Web Host...");

    var builder = WebApplication.CreateBuilder(args);

    // Підключення Serilog до хоста (перехоплює всі логи .NET)
    builder.Host.UseSerilog();

    // 2. Підключення Бази Даних
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    // Перевірка на випадок, якщо забув додати рядок підключення
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    builder.Services.AddDbContext<GoldenEuropeDbContext>(options =>
        options.UseSqlServer(connectionString));

    // 3. Реєстрація DI (Services & Repositories)
    builder.Services.AddScoped<ICarRepository, CarRepository>();
    builder.Services.AddScoped<ICarService, CarService>();

    // 4. AutoMapper
    builder.Services.AddAutoMapper(typeof(CarService).Assembly);

    // 5. FluentValidation
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<CreateCarDtoValidator>();

    // Налаштування кастомної відповіді для помилок валідації
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
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
        };
    });

    // 6. CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp", policy =>
        {
            // Переконайся, що порти правильні (React зазвичай 5173 або 3000)
            policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    builder.Services.AddControllers();

    // 7. API Versioning
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }).AddMvc().AddApiExplorer(options =>
    {
        // Це важливо для Swagger, щоб він розумів версії v1, v2
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    // 8. Swagger Configuration
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GoldenEurope API", Version = "v1" });
        
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization", Type = SecuritySchemeType.Http, Scheme = "Bearer",
            BearerFormat = "JWT", In = ParameterLocation.Header,
            Description = "Enter your valid token in the text input below."
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                }, Array.Empty<string>()
            }
        });
    });

    var app = builder.Build();

    // --- Middleware Pipeline ---

    app.UseSerilogRequestLogging(); // Логування HTTP запитів

    // Swagger має працювати ТІЛЬКИ якщо ми не в Production (або якщо ти явно дозволиш)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => 
        {
            // Явно вказуємо шлях до JSON, іноді це допомагає якщо Swagger не вантажиться
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoldenEurope API v1");
        });
    }

    app.UseMiddleware<GlobalExceptionHandler>();

    app.UseHttpsRedirection();
    
    app.UseCors("AllowReactApp");
    
    app.UseAuthorization();
    
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // Якщо помилка на старті (наприклад, DB Connection), ми побачимо її в консолі
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}