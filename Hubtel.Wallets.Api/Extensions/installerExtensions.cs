using System;
using System.Collections.Generic;
using Hubtel.Wallets.Api.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Hubtel.Wallets.Api.Extensions
{
    /// <summary>
    /// An extension class for the IServiceCollection comtract
    /// </summary>
    public static class InstallerExtensions
    {
        /// <summary>
        /// Method excutes all class implementation of the IInstaller interface which are not abstracts or interfaces themselves.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns>Task</returns>
        public static void InstallServicesInAssembly(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            List<IInstaller> Installers = typeof(Program).Assembly.ExportedTypes.Where(
                    exportedType =>
                        typeof(IInstaller).IsAssignableFrom(exportedType)
                        && !exportedType.IsInterface
                        && !exportedType.IsAbstract
                )
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            Installers.ForEach(Installer => Installer.InstallServices(services, configuration));
        }
    }
}
