using Ardalis.Result;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.TransactionAgregate;


namespace TrackFinance.Core.Services;


public class ByDateStrategy : ITransactionStrategy
{
    public DateType SupportedDateType => DateType.Date;

    public ByDateStrategy()
    {
    }
    public Result<List<TransactionDataDto>> GetTransactionItemsAsync(List<Transaction> itemsByDay, CancellationToken cancellationToken = default)
    {
        var list = new List<TransactionDataDto>();
        var selectedValues = itemsByDay.Select(t => new
        {
            t.ExpenseDate.Date,
            t.ExpenseDate.Year,
            t.ExpenseDate.Month,
            t.Amount,
            t.TransactionDescriptionType,
            t.TransactionType
        })
              .GroupBy(h => new { h.Year, h.Month, h.TransactionDescriptionType, h.TransactionType })
               .Select(g => new
               {
                   g.Key.Year,
                   g.Key.Month,
                   TotalAmount = g.Sum(h => h.Amount),
                   g.Key.TransactionDescriptionType,
                   g.Key.TransactionType
               })
        .OrderBy(g => g.Year)
        .ThenBy(g => g.Month)
        .ToList();

        foreach (var t in selectedValues)
        {
            var transactionItem = new TransactionDataDto
            {
                TotalAmount = t.TotalAmount,
                Month = t.Month,
                Year = t.Year,
                TransactionDescriptionType = t.TransactionDescriptionType,
                TransactionType = t.TransactionType
            };
            list.Add(transactionItem);
        }
        if (list.Count == 0) Result<List<TransactionDataDto>>.NotFound();
        return list;
    }

    public Result<List<TransactionDataDto>> GetTransactionItemsForLineChartsAsync(List<Transaction> itemByDay, CancellationToken cancellationToken = default)
    {
        var transactions = new List<TransactionDataDto>();
        var selectedValues = itemByDay.Select(t => new
        {
            t.ExpenseDate.Date,
            t.Amount,
            t.TransactionDescriptionType,
            t.TransactionType
        })
              .GroupBy(t => new { t.TransactionType, t.Date })
              .Select(y => new { Date = y.Key, TotalAmount = y.Sum(x => x.Amount), y.Key.TransactionType })
              .OrderBy(x => x.Date.Date)
              .ToList();

        foreach (var t in selectedValues)
        {
            var transactionItem = new TransactionDataDto
            {
                Date = t.Date.Date,
                TotalAmount = t.TotalAmount,
                TransactionType = t.TransactionType
            };

            transactions.Add(transactionItem);
        }
        if (transactions.Count == 0) Result<List<TransactionDataDto>>.NotFound();

        return transactions;
    }
}


