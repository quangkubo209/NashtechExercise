using QuangNN_ASPWebAPI.DTOs;
using QuangNN_ASPWebAPI.Repositories;
using QuangNN_ASPWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();
builder.Services.AddSingleton<ITodoTaskRepository, TodoTaskRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperTask));


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
