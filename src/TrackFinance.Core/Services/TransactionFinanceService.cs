using System.Globalization;
using Ardalis.Result;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate.Specifications;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Core.Services;

public class TransactionFinanceService : ITransactionFinanceService
{
  private readonly IRepository<Transaction> _repository;
  private readonly IEnumerable<ITransactionStrategy> _strategies;
  public TransactionFinanceService(IRepository<Transaction> repository, IEnumerable<ITransactionStrategy> strategies)
  {
    _repository = repository;
    _strategies = strategies;
  }

  public async Task<Result<List<TransactionDataDto>>> GetTransactionItemsByAsync(DateType dateType,
  int userId, TransactionType transactionType, CancellationToken cancellationToken = default)
  {
    var _transactionStrategy = _strategies.Single(s => s.SupportedDateType == dateType);

    switch (dateType)
    {
      case DateType.Day:
        var itemByDay = await _repository.ListAsync(new ItemsByDaySpec(userId, transactionType), cancellationToken);
        return _transactionStrategy.GetTransactionItemsAsync(itemByDay, cancellationToken);
      case DateType.Week:
        var itemByWeek = await _repository.ListAsync(new ItemsByWeekSpec(userId, transactionType), cancellationToken);
        return _transactionStrategy.GetTransactionItemsAsync(itemByWeek, cancellationToken);
      case DateType.Month:
        var itemByMonth = await _repository.ListAsync(new ItemsByMonthSpec(userId, transactionType), cancellationToken);
        return _transactionStrategy.GetTransactionItemsAsync(itemByMonth, cancellationToken);
      default: return Result<List<TransactionDataDto>>.NotFound();
    }
  }

  public async Task<Result<List<TransactionDataDto>>> GetTransactionItemsByDateAsync(DateType dateType, int userId, TransactionType transactionType, DateTime starDate, DateTime endDate, CancellationToken cancellationToken = default)
  {
    var _transactionStrategy = _strategies.Single(s => s.SupportedDateType == dateType);
    var itemByDay = await _repository.ListAsync(new ItemsByDateSpec(userId, transactionType, starDate, endDate), cancellationToken);
    return _transactionStrategy.GetTransactionItemsAsync(itemByDay, cancellationToken);
  }

  public async Task<Result<List<TransactionDataDto>>> GetTransactionItemsForLineChartsAsync(DateType dateType, int userId, TransactionType transactionType, CancellationToken cancellationToken = default)
  {
    var _transactionStrategy = _strategies.Single(s => s.SupportedDateType == dateType);

    switch (dateType)
    {
      case DateType.Day:
        var itemByDay = await _repository.ListAsync(new ItemsByDaySpec(userId, transactionType), cancellationToken);
        return _transactionStrategy.GetTransactionItemsForLineChartsAsync(itemByDay, cancellationToken);

      case DateType.Week:
        var itemByWeek = await _repository.ListAsync(new ItemsByWeekSpec(userId, transactionType), cancellationToken);
        return _transactionStrategy.GetTransactionItemsForLineChartsAsync(itemByWeek, cancellationToken);

      case DateType.Month:
        var itemByMonth = await _repository.ListAsync(new ItemsByMonthSpec(userId, transactionType), cancellationToken);
        return _transactionStrategy.GetTransactionItemsForLineChartsAsync(itemByMonth, cancellationToken);

      default: return Result<List<TransactionDataDto>>.NotFound();
    }
  }



}
