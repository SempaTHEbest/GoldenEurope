using GoldenEurope.Business.DTOs.Auth;

namespace GoldenEurope.Business.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}