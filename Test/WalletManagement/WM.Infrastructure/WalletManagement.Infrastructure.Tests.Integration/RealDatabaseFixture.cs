using System.Transactions;
using Microsoft.EntityFrameworkCore;
using WalletManagement.Infrastructure.EFCore;

namespace WalletManagement.Infrastructure.Tests.Integration;

public class RealDatabaseFixture : IDisposable
{
    public WalletContext Context;
    private readonly TransactionScope _scope;
    public RealDatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<WalletContext>()
            .UseSqlServer("Data Source=.\\SQLEXPRESS ; Initial Catalog=EditorLand ; Trusted_Connection=True")
            .Options;
        Context = new WalletContext(options);
        _scope = new TransactionScope();
    }
    public void Dispose()
    {
        Context.Dispose();
        _scope.Dispose();
    }
}