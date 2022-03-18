using AuthApi.Infrastructure;
using AuthApi.Infrastructure.RefreshToken;
using AuthApi.Model;
using AuthApi.Service.Auth;
using AuthApi.Service.RefreshToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
//builder.Services.AddHttpContextAccessor();

// Update Jwt config from the settings
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:sqlConnection").Value));

// converte secret key no appsetting.json para Bytes
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true, // vai validar a chave secreta
    IssuerSigningKey = new SymmetricSecurityKey(key), // a key é encriptada aqui
    ValidateIssuer = false, // 
    ValidateAudience = false,
    ValidateLifetime = true,
    RequireExpirationTime = false,
    //ClockSkew = TimeSpan.Zero // se não tiver esta validação, a duração do token é 5min por defeito
};

// injeta para DepenInjection container
builder.Services.AddSingleton(tokenValidationParams);

builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
                .AddEntityFrameworkStores<AuthDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
