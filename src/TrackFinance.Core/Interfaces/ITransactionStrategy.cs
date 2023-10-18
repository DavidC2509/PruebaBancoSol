using Ardalis.Result;
using TrackFinance.Core.Services;
using TrackFinance.Core.TransactionAgregate;
namespace TrackFinance.Core.Interfaces;

public interface ITransactionStrategy
{
    DateType SupportedDateType { get; }

    Result<List<TransactionDataDto>> GetTransactionItemsAsync(List<Transaction> items, CancellationToken cancellationToken = default);


    Result<List<TransactionDataDto>> GetTransactionItemsForLineChartsAsync(List<Transaction> items, CancellationToken cancellationToken = default);

}