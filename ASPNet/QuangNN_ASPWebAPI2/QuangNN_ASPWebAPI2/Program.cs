using API.Services;
using API.Services.Validations;
using Infrastructure.Repositories;
using QuangNN_ASPWebAPI2.API.DTOs;
using QuangNN_ASPWebAPI2.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPersonService, PersonService>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperPerson));
builder.Services.AddScoped<IValidationPerson, ValidationPerson>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
