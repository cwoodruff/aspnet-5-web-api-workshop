﻿using System;
using System.Runtime.InteropServices;
using Chinook.DataEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chinook.Domain.DbInfo;
using Microsoft.Data.SqlClient;

namespace Chinook.API.Configurations
{
    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connection = String.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = configuration.GetConnectionString("ChinookDbWindows") ??
                             "Server=.;Database=Chinook;Trusted_Connection=True;Application Name=ChinookASPNETCoreAPINTier";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                     RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = configuration.GetConnectionString("ChinookDbDocker") ??
                             "Server=localhost,1433;Database=Chinook;User=sa;Password=P@55w0rd;Trusted_Connection=False;Application Name=ChinookASPNETCoreAPINTier";
            }

            services.AddDbContextPool<ChinookContext>(options => options.UseSqlServer(connection));

            services.AddSingleton(new SqlConnection(connection));

            services.AddSingleton(new DbInfo(connection));

            return services;
        }
    }
}