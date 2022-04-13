using Microsoft.EntityFrameworkCore;
using PetApi.Infrastructure;
using PetApi.Infrastructure.Breed;
using PetApi.Infrastructure.Pet;
using PetApi.Infrastructure.Type;
using PetApi.Service.Breed;
using PetApi.Service.Pet;
using PetApi.Service.Type;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPetRepository, PetRepository>();
builder.Services.AddTransient<IPetService, PetService>();
builder.Services.AddTransient<IBreedRepository, BreedRepository>();
builder.Services.AddTransient<IBreedService, BreedService>();
builder.Services.AddTransient<ITypeRepository, TypeRepository>();
builder.Services.AddTransient<ITypeService, TypeService>();

builder.Services.AddDbContext<PetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:sqlConnection").Value));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    serviceScope.ServiceProvider.GetRequiredService<PetDbContext>().Database.Migrate();
}

app.Run();

// to be able to run Program class in specflow
public partial class Program { }