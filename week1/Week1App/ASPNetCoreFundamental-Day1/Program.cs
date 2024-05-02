//using ASPNetCoreFundamental_Day1;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Hosting;

//namespace ASPNetCoreFundamental_Day1
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}
using ASPNetCoreFundamental_Day1;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IWriteMessage, WriteMessage>();
//configure for swagger 
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
