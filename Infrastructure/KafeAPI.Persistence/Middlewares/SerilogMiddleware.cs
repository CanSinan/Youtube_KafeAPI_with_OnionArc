using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace KafeAPI.Persistence.Middlewares
{
    public class SerilogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SerilogMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            var request = context.Request;
            var ip = context.Connection.RemoteIpAddress?.ToString();
            //var username = context.User.Identity?.Name ?? "Anonim";
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("_e");
            var username = claim != null ? claim.Value:"Anonim";
            var requestPath = request.Path;

            using (LogContext.PushProperty("Username", username))
            using (LogContext.PushProperty("RequestPath", requestPath))
            using (LogContext.PushProperty("RequestMethod", request.Method))
            using (LogContext.PushProperty("RequestIP", ip))
            {
                Log.Information("Gelen istek: {Method} {Path} - IP: {IP}", request.Method, request.Path, ip);

                try
                {
                    await _next(context);
                    sw.Stop();
                    Log.Information("Yanıt: {StatusCode} - Sure: {Elapsed} ms", context.Response.StatusCode, sw.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    Log.Error(ex, "Hata oluştu. Sure: {Elapsed} ms", sw.ElapsedMilliseconds);
                    throw;
                }
            }


        }
    }
}
