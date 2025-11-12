using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Authentication.Application.DTOs.Auth;
using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Wrapper;

namespace Authentication.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<UserDbTable> _userManager;
    private readonly SignInManager<UserDbTable> _signInManager;
    private readonly IConfiguration _config;
    private readonly RsaSecurityKey _privateKey;

    public AuthService(
        UserManager<UserDbTable> userManager,
        SignInManager<UserDbTable> signInManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
        var privateKeyPath = _config["Jwt:PrivateKeyPath"];
        var rsa = RSA.Create();
        rsa.ImportFromPem(File.ReadAllText(privateKeyPath!));
        _privateKey = new RsaSecurityKey(rsa);
    }

    public async Task<ApiResponse<AuthResultDto>> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userManager.FindByNameAsync(dto.Username);
        if (existingUser != null)
            return ApiResponse<AuthResultDto>.Fail("Username already exists");

        var user = new UserDbTable
        {
            UserName = dto.Username,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return ApiResponse<AuthResultDto>.Fail(
                string.Join("; ", result.Errors.Select(e => e.Description)));

        var accessToken = CreateAccessTokenAsync(user);

        var dtoOut = new AuthResultDto
        {
            AccessToken = accessToken,
        };

        return ApiResponse<AuthResultDto>.Ok(dtoOut, "User registered successfully");
    }

    public async Task<ApiResponse<AuthResultDto>> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user == null)
            return ApiResponse<AuthResultDto>.Fail("Invalid username or password");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            return ApiResponse<AuthResultDto>.Fail("Invalid username or password");

        var accessToken = CreateAccessTokenAsync(user);

        var dtoOut = new AuthResultDto
        {
            AccessToken = accessToken,
        };

        return ApiResponse<AuthResultDto>.Ok(dtoOut, "Login successful");
    }

    private string CreateAccessTokenAsync(UserDbTable user)
    {
        var jwtConf = _config.GetSection("Jwt");
        var issuer = jwtConf["Issuer"];
        var audience = jwtConf["Audience"];
        var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtConf["AccessTokenExpirationMinutes"]!));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var creds = new SigningCredentials(_privateKey, SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}