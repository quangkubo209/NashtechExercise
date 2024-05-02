using ASPNetCoreFundamental_Day1;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IWriteMessage, WriteMessage>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.MapGet("/api/", () => "Hello World!");
app.MapPost("/api/post", async (HttpContext context, IWriteLog writeLog) =>
{
    var name = context.Request.Form["name"];
    var age = int.Parse(context.Request.Form["age"]);

    writeLog.WriteLog($"Post message successfully: Name={name}, Age={age}");

    await context.Response.WriteAsync("User logged successfully");
});

app.UseMiddleware<CustomMiddleware>();
app.Run();
