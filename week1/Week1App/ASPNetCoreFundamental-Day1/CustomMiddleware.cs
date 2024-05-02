using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreFundamental_Day1;
public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public CustomMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, IWriteMessage writeMessage)
    {
        string directoryPath = _environment.ContentRootPath;
        string filePathName = Path.Combine(directoryPath, "request_logs.txt");
        string schema = context.Request.Scheme;
        string host = context.Request.Host.ToString();
        string path = context.Request.Path;
        string queryString = context.Request.QueryString.ToString();
        string requestBody = await ReadRequestBody(context.Request);

        string logMessage = $"{DateTime.Now}: Schema={schema}, Host={host}, Path={path}, QueryString={queryString}, RequestBody={requestBody}\n";

        writeMessage.WriteMessage(logMessage, filePathName);

        await _next(context);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
        {
            string body = await reader.ReadToEndAsync();
            return body;
        }
    }

}
