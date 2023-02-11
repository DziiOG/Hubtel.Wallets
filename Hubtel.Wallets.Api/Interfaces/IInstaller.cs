using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hubtel.Wallets.Api.Interfaces
{
    
     public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}