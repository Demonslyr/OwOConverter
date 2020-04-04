using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OwOConverter.StringExtensions.OwOConverter;

namespace OwOConverter
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/{text}", async context =>
                {
                    var resultString = "Send a string in the url!";
                    try
                    {
                        var inputString = context.GetRouteValue("text").ToString();
                        if (!string.IsNullOrWhiteSpace(inputString))
                            resultString = inputString;
                    }
                    catch (Exception e) { }
                    finally
                    {
                        await context.Response.WriteAsync(resultString.ConvertToOwO());
                    }
                });
            });
        }
    }
}
