using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WalletManagement.Application;
using WalletManagement.Application.Contracts.Transaction;
using WalletManagement.Application.Contracts.Wallet;
using WalletManagement.Domain.TransactionAgg;
using WalletManagement.Domain.WalletAgg;
using WalletManagement.Infrastructure.EFCore;
using WalletManagement.Infrastructure.EFCore.Repositories;

namespace WalletManagement.Infrastructure.Configuration;

public class WalletManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IWalletRepository, WalletRepository>();
        services.AddTransient<IWalletApplication, WalletApplication>();

        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<ITransactionApplication, TransactionApplication>();

        services.AddDbContext<WalletContext>(v => v.UseSqlServer(connectionString));
    }
}