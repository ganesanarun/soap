using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiClient
{
    using System;
    using System.ServiceModel;
    using CustomMiddleware;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(ServiceClient());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        private static ISampleService ServiceClient()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri("http://localhost:5000/service.svc"));
            var channelFactory = new ChannelFactory<ISampleService>(binding, endpoint);
            var serviceClient = channelFactory.CreateChannel();
            return serviceClient;
        }
    }
}
