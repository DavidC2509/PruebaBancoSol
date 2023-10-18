using Ardalis.Result;
using TrackFinance.Core.Services;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.Interfaces;
public interface ITransactionFinanceService
{
  public Task<Result<List<TransactionDataDto>>> GetTransactionItemsByAsync(DateType dateType, int userId, TransactionType transactionType, CancellationToken cancellationToken = default);
  Task<Result<List<TransactionDataDto>>> GetTransactionItemsForLineChartsAsync(DateType dateType, int userId, TransactionType transactionType, CancellationToken cancellationToken = default);

  Task<Result<List<TransactionDataDto>>> GetTransactionItemsByDateAsync(DateType dateType, int userId, TransactionType transactionType, DateTime starDate, DateTime endDate, CancellationToken cancellationToken = default);

}
