using AuthApiClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PetsExchangeApi.Service.Auth;
using PetsExchangeApi.Service.User;
using System.Text;
using UserApiClient; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserApiClient, UserApiClient.UserApiClient>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IAuthApiClient, AuthApiClient.AuthApiClient>();

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
    //ClockSkew = TimeSpan.Zero
};

// injeta para DepenInjection container
builder.Services.AddSingleton(tokenValidationParams);

// define que a autenticação tem de passar pelo JWTtokens
builder.Services.AddAuthentication(options =>
{
    // este é o 1º schema
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // este é o 2º para o caso do primeiro falhar
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt => {
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = tokenValidationParams;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
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
