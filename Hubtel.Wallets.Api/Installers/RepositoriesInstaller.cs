using Hubtel.Wallets.Api.Repositories;
using Hubtel.Wallets.Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Hubtel.Wallets.Api.Intallers
{
    public class RepositoriesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IWalletRepository, WalletRepository>();
        }
    }
}
