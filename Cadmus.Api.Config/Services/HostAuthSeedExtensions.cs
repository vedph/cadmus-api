﻿using Cadmus.Api.Services.Seeding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Cadmus.Api.Config.Services;

/// <summary>
/// Database initializer extension to <see cref="IHost"/>.
/// See https://stackoverflow.com/questions/45148389/how-to-seed-in-entity-framework-core-2.
/// </summary>
public static class HostAuthSeedExtensions
{
    private static Task SeedAsync(IServiceProvider serviceProvider)
    {
        AuthDatabaseInitializer initializer = new(serviceProvider);

        return Policy.Handle<DbException>()
            .WaitAndRetry(
            [
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(30),
                TimeSpan.FromSeconds(60)
            ], (exception, timeSpan, _) =>
            {
                ILogger logger = serviceProvider
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger(typeof(HostAuthSeedExtensions));

                string message = "Unable to connect to DB" +
                    $" (sleep {timeSpan}): {exception.Message}";
                Console.WriteLine(message);
                logger.LogError(exception, message);
            }).Execute(async () =>
            {
                await initializer.SeedAsync();
            });
    }

    /// <summary>
    /// Seeds the database.
    /// </summary>
    /// <param name="host">The host.</param>
    /// <returns>The received host, to allow concatenation.</returns>
    /// <exception cref="ArgumentNullException">serviceProvider</exception>
    public static async Task<IHost> SeedAuthAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        ILogger logger = serviceProvider
            .GetRequiredService<ILoggerFactory>()!
            .CreateLogger(typeof(HostAuthSeedExtensions));

        try
        {
            IConfiguration config =
                serviceProvider.GetRequiredService<IConfiguration>();

            int delay = config.GetValue<int>("Seed:Delay");
            if (delay > 0)
            {
                logger.LogInformation("Waiting {Delay} seconds...", delay);
                Thread.Sleep(delay * 1000);
            }
            else
            {
                logger.LogInformation("No seed delay set.");
            }

            await SeedAsync(serviceProvider);
            return host;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error seeding auth database: {Error}", ex.Message);
            throw;
        }
    }
}
