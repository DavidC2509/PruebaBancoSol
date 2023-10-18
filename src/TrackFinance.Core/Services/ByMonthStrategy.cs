using Ardalis.Result;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.TransactionAgregate;


namespace TrackFinance.Core.Services;


public class ByMonthStrategy : ITransactionStrategy
{
    public DateType SupportedDateType => DateType.Month;

    public ByMonthStrategy()
    {
    }
    public Result<List<TransactionDataDto>> GetTransactionItemsAsync(List<Transaction> itemByMonth, CancellationToken cancellationToken = default)
    {
        var list = new List<TransactionDataDto>();

        var items = itemByMonth.Select(h => new
        {
            h.ExpenseDate.Year,
            h.ExpenseDate.Month,
            h.Amount,
            h.TransactionType,
            h.TransactionDescriptionType,
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

        foreach (var t in items)
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

    public Result<List<TransactionDataDto>> GetTransactionItemsForLineChartsAsync(List<Transaction> itemByMonth, CancellationToken cancellationToken = default)
    {
        var list = new List<TransactionDataDto>();

        var items = itemByMonth.Select(h => new
        {
            h.ExpenseDate.Year,
            h.ExpenseDate.Month,
            h.Amount,
            h.TransactionType,
            h.TransactionDescriptionType,
        })
        .GroupBy(h => new { h.Year, h.Month, h.TransactionType })
        .Select(g => new
        {
            g.Key.Year,
            g.Key.Month,
            TotalAmount = g.Sum(h => h.Amount),
            g.Key.TransactionType
        })
        .OrderBy(g => g.Year)
        .ThenBy(g => g.Month)
        .ToList();

        foreach (var t in items)
        {
            var transactionItem = new TransactionDataDto
            {
                TotalAmount = t.TotalAmount,
                Month = t.Month,
                Year = t.Year,
                TransactionType = t.TransactionType
            };
            list.Add(transactionItem);
        }
        if (list.Count == 0) Result<List<TransactionDataDto>>.NotFound();
        return list;
    }
}


