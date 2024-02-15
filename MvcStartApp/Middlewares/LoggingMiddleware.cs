using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MvcStartApp.Repositories;

namespace MvcStartApp.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IRequestRepository requestRepository;

        private const string PathToLogs = "logs/RequestLog.txt";

        public LoggingMiddleware(RequestDelegate next, IRequestRepository requestRepository)
        {
            this.next = next;
            this.requestRepository = requestRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var log = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}";

            LogToConsole(log);
            await LogToFile(log);
            await LogToDatabase(log);

            await next.Invoke(context);
        }

        private void LogToConsole(string log)
        {
            Console.WriteLine(log);
        }

        private async Task LogToFile(string log)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            
            await using var writer = new StreamWriter(PathToLogs, true);
            await writer.WriteLineAsync(log);
        }

        private async Task LogToDatabase(string log)
        {
            await requestRepository.AddRequest(log);
        }
    }
}