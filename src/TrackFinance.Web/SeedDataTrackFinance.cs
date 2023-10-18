using Microsoft.EntityFrameworkCore;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Infrastructure.Data;

namespace TrackFinance.Web;

public static class SeedDataTrackFinance
{
  public static readonly Transaction TestTransaction = Transaction.CreateExpenses("compra de equipos", 20, TransactionDescriptionType.Services, Convert.ToDateTime("2023-08-01T20:31:50.647Z"), 1);
  public static readonly Transaction TestTransaction1 = Transaction.CreateExpenses("compra computadoras", 10, TransactionDescriptionType.Services, Convert.ToDateTime("2023-08-31T20:31:50.647Z"), 1);
  public static readonly Transaction TestTransaction2 = Transaction.CreateExpenses("compra accesorios de gimnasio", 10, TransactionDescriptionType.Services, DateTime.Now, 1);
  public static readonly Transaction TestTransaction3 = Transaction.CreateExpenses("compra joyeria", 10, TransactionDescriptionType.Services, DateTime.Now, 1);
  public static readonly Transaction TestTransaction4 = Transaction.CreateIncomes("venta comida", 30, TransactionDescriptionType.Food, DateTime.Now, 1);
  public static readonly Transaction TestTransaction5 = Transaction.CreateIncomes("venta desayuno", 25, TransactionDescriptionType.Food, DateTime.Now, 1);

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
    PopulateTestData(dbContext);
  }
  
  public static void PopulateTestData(AppDbContext dbContext)
  {
    AddIfNotExists(dbContext, TestTransaction);
    AddIfNotExists(dbContext, TestTransaction1);
    AddIfNotExists(dbContext, TestTransaction2);
    AddIfNotExists(dbContext, TestTransaction3);
    AddIfNotExists(dbContext, TestTransaction4);
    AddIfNotExists(dbContext, TestTransaction5);
    dbContext.SaveChanges();
  }

  private static void AddIfNotExists(AppDbContext dbContext, Transaction transaction)
  {
    // Verificar si la transacción ya existe en la base de datos
    if (!dbContext.Transactions.Any(t => t.Description == transaction.Description))
    {
      // La transacción no existe, por lo que la agregamos
      dbContext.Transactions.Add(transaction);
    }
  }
}
