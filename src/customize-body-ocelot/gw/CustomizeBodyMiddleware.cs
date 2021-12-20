using gw.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gw
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomizeBodyMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomizeBodyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (!HttpMethods.IsGet(context.Request.Method)
         && !HttpMethods.IsHead(context.Request.Method)
         && !HttpMethods.IsDelete(context.Request.Method)
         && !HttpMethods.IsTrace(context.Request.Method)
         && context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
                await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                var bodyAsText = Encoding.UTF8.GetString(buffer);

                var dataFromClientRequest = JsonConvert.DeserializeObject<MyTodoItem>(bodyAsText);
                var dataToRealServer = new TodoItem
                {
                    Completed = dataFromClientRequest.Finito,
                    Id = dataFromClientRequest.MyId,
                    UserId = dataFromClientRequest.MyUserId,
                    Title = dataFromClientRequest.MyTitle
                };
                var requestContent = new StringContent(JsonConvert.SerializeObject(dataToRealServer), Encoding.UTF8, "application/json");
                context.Request.Body = await requestContent.ReadAsStreamAsync();
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomizeBodyMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomizeBodyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomizeBodyMiddleware>();
        }
    }
}
