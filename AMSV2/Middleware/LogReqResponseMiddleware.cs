using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMSV2.Middleware
{
    public class LogReqResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDiagnosticContext _diagnosticContext;

        public LogReqResponseMiddleware(RequestDelegate next, IDiagnosticContext diagnosticContext)
        {
            _next = next;
            _diagnosticContext = diagnosticContext;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<LogReqResponseMiddleware> logger)
        {
            var request = httpContext.Request;
            request.EnableBuffering();
            // 获取Token信息
            string loginName = httpContext.User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
            string addressIP = httpContext.Connection.RemoteIpAddress.ToString();
            //把请求body流转换成字符串
            string bodyAsText = await new StreamReader(request.Body).ReadToEndAsync();//记录请求信息
            _diagnosticContext.Set("PARAMS", $"{request.QueryString} {bodyAsText}");
            var requestStr = $"请求用户名：{loginName} 请求人IP：{addressIP} 协议：{request.Scheme} 主机路径：{request.Host}{request.Path} 参数：{request.QueryString} {bodyAsText}Token：{request.Headers["Authorization"]}";
            logger.LogDebug($"客户端请求【{requestStr}】");
            request.Body.Seek(0, SeekOrigin.Begin);
            var originalBodyStream = httpContext.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;
                await _next(httpContext);
                var response = httpContext.Response;
                response.Body.Seek(0, SeekOrigin.Begin);
                //转化为字符串
                string text = await new StreamReader(response.Body).ReadToEndAsync();
                //从新设置偏移量0
                response.Body.Seek(0, SeekOrigin.Begin);
                //记录返回值
                var responsestr = $"状态：{response.StatusCode} 内容：{text}";
                logger.LogDebug($"返回【{responsestr}】");
                await responseBody.CopyToAsync(originalBodyStream);
            }
            //return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LogReqResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogReqResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogReqResponseMiddleware>();
        }
    }
}
