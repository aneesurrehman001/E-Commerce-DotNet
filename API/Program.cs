using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            /* what we need to do is go and get access to our data context.
               But what we going to do is because we're outside of our services container
               in the startup class where we don't have control over the lifetime of when we 
               create this particular instance of our context, we're going to do this inside.
               A using statement and a using statement means that any code that runs inside of
               this is going to be disposed of as soon as we've finished with the methods inside
               that we don't need to worry about cleaning up after ourselves.

            */
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                // logger is for log any information on our console.
                // We are going to create an instance of logger Factory and
                // LoggerFactory allows us to create instances of our class.
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    /* this migrates async commands does this is going to apply
                       any pending migration's for a context to the database and 
                       it will create the database if it does not already exist. */
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    //"Program" is the class we want to log against.
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during migration.");

                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
