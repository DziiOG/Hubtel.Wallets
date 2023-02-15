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
        /// This method allows for modular service installation by scanning the assembly for installers that implement the IInstaller interface.
        /// This makes it easy to add or remove services by simply adding or removing an installer.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns>A task representing the completion of the service installation.</returns>
        public static void InstallServicesInAssembly(
            this IServiceCollection services, // The IServiceCollection to install services into
            IConfiguration configuration // The IConfiguration object containing application configuration properties
        )
        {
            // Get all exported types in the assembly that contains the Program class
            // Filter the list to only include types that implement IInstaller, are not abstract or interface types
            List<IInstaller> Installers = typeof(Program).Assembly.ExportedTypes
                .Where(
                    exportedType =>
                        typeof(IInstaller).IsAssignableFrom(exportedType) // Check if type implements IInstaller
                        && !exportedType.IsInterface // Check if type is not an interface
                        && !exportedType.IsAbstract // Check if type is not abstract
                )
                .Select(Activator.CreateInstance) // Create an instance of each filtered type
                .Cast<IInstaller>() // Cast the instance to the IInstaller interface
                .ToList(); // Convert the filtered and cast instances to a List<IInstaller>

            // Call InstallServices on each IInstaller instance in the list to install services into the IServiceCollection
            Installers.ForEach(Installer => Installer.InstallServices(services, configuration));
        }
    }
}
