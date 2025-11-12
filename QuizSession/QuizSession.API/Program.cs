using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using QuizSession.API.Hubs;
using QuizSession.Application.Services.BackgroundProcessor;
using QuizSession.Application.Services.QuizSession;
using QuizSession.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] = []
    });
});
builder.Services.AddDbContext<QuizSessionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IQuizSessionService, QuizSessionService>();
builder.Services.AddScoped<IQuizSessionBackgroundProcessor, QuizSessionBackgroundProcessor>();

builder.Services.AddQuartz();
builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
builder.Services.AddSignalR();var jwtConfig = builder.Configuration.GetSection("Jwt");

var publicKeyPath = jwtConfig["PublicKeyPath"];
var rsa = RSA.Create();
rsa.ImportFromPem(File.ReadAllText(publicKeyPath));
var key = new RsaSecurityKey(rsa);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtConfig["Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<QuizSessionHub>("/hubs/quiz-session");

app.MapControllers();
app.Run();