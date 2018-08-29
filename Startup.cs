using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class SomeSingltonService { }

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddSingleton<SomeSingltonService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        var appServices = app.ApplicationServices;
        var appService = appServices.GetRequiredService<SomeSingltonService>();

        Console.WriteLine("=======================");
        Console.WriteLine("Configure");

        app.UseMvc(configureRoutes =>
        {
            var routeServices = configureRoutes.ServiceProvider;
            var routeService = routeServices.GetRequiredService<SomeSingltonService>();

            Console.WriteLine("UseMvc");

            if (appServices == routeServices && appService == routeService)
            {
                Console.WriteLine("They are the same instances.");
            }
        });

        Console.WriteLine("=======================");
    }
}