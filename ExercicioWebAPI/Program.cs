using ExercicioWebAPI.Configurations;
using Database.Context;
using Repository;
using Repository.Interfaces;
using Services;
using Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using Microsoft.OpenApi.Models;
using Authentication.Interfaces;
using Authentication;
using DTOs.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Identity
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>()
    .AddDefaultTokenProviders();

// Configure DI for Identity service
builder.Services.AddScoped<IIdentityService, IdentityService>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(UserProfile).Assembly);


// Configure repositories
builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IAmostraRepository, AmostraRepository>();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();

// Configure services
builder.Services.AddScoped<IAmostraService, AmostraService>();

builder.Services.AddScoped<ITokenService, TokenService>();

// Configure Entity Framework Contexts
builder.Services.AddDbContext<ExcWebAPIContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"),
        assembly => assembly.MigrationsAssembly(typeof(ExcWebAPIContext).Assembly.FullName));
});

builder.Services.AddDbContext<IdentityDataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"),
        assembly => assembly.MigrationsAssembly(typeof(IdentityDataContext).Assembly.FullName));
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Cabe�alho de autoriza��o JWT usando o esquema Bearer.
                        Insira 'Bearer' [espa�o] e, em seguida, seu token no campo de texto abaixo.
                        Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
});

// Configure authentication
var jwtAppSettingOptions = builder.Configuration.GetSection(nameof(JwtOptions));
var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtOptions:SecurityKey").Value));

builder.Services.Configure<JwtOptions>(options =>
{
    options.Issuer = jwtAppSettingOptions[nameof(JwtOptions.Issuer)];
    options.Audience = jwtAppSettingOptions[nameof(JwtOptions.Audience)];
    options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
    options.Expiration = int.Parse(jwtAppSettingOptions[nameof(JwtOptions.Expiration)] ?? "0");
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
});

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration.GetSection("JwtOptions:Issuer").Value,

    ValidateAudience = true,
    ValidAudience = builder.Configuration.GetSection("JwtOptions:Audience").Value,

    ValidateIssuerSigningKey = true,
    IssuerSigningKey = securityKey,

    RequireExpirationTime = true,
    ValidateLifetime = true,

    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = tokenValidationParameters;
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
