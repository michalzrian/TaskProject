using System.Diagnostics;
using System;
using System.Threading.Tasks; // Added for Task
using Microsoft.AspNetCore.Builder; // Added for IApplicationBuilder
using Microsoft.AspNetCore.Http; // Added for HttpContext
using Microsoft.Extensions.Logging; // Added for ILogger
using System.IO;
namespace פרויקט
{
      public class LogMiddleware
    {
        private readonly RequestDelegate next;
        //private readonly ILogger logger;
        private readonly string file;
      //  במקום כתיבה ללוג כתיבה לקובץ//
        //ILogger<LogMiddleware> logger
        public LogMiddleware(RequestDelegate next,string file)
        {
            this.next = next;
            this.file = file;
           // this.logger = logger;
        }

        public async Task Invoke(HttpContext c)
        {
            var act = $"{c.Request.Path}.{c.Request.Method}";
            var sw = new Stopwatch();
            sw.Start();
            string Message =$"{act} started at {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}"; 
            writeFile(Message);
            //logger.LogInformation($"{act} started at {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            await next.Invoke(c);
            Message = $"{act} ended at {sw.ElapsedMilliseconds} ms."+ $" User: {c.User?.FindFirst("userId")?.Value ?? "unknown"}";
            writeFile(Message);
            //logger.LogDebug($"{act} ended at {sw.ElapsedMilliseconds} ms."
           // + $" User: {c.User?.FindFirst("userId")?.Value ?? "unknown"}");
        
        }
        private void writeFile(string Message)
        {
            using(StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(Message);
            }
        }
    }

    public static partial class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder,string nameFile)
        {
            return builder.UseMiddleware<LogMiddleware>(nameFile);
        }
    }
}