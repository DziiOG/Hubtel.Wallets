using Hubtel.Wallets.Api.Contracts.RequestDtos;
using Hubtel.Wallets.Api.Interfaces;
using Hubtel.Wallets.Api.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Hubtel.Wallets.Api.Intallers
{
    public class ValidatorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // id required
            services.AddScoped<IValidator<string>, ObjectIdValidator>();

            // wallet validations
            services.AddScoped<IValidator<WalletQueryDto>, QueryWalletValidator>();
            services.AddScoped<IValidator<CreateWalletDto>, CreateWalletValidator>();
            services.AddScoped<IValidator<PatchWalletDto>, PatchWalletValidator>();
        }
    }
}
