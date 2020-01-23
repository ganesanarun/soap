namespace CustomMiddleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class SoapEndpointMiddleware
    {
        private readonly RequestDelegate next;

        public SoapEndpointMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine($"Request for {context.Request.Path} received ({context.Request.ContentLength ?? 0} bytes)");
            await next.Invoke(context);
        }
    }
}
