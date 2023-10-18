using Ardalis.Specification;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.TransactionAgregate.Specifications;
public class ItemsByDateSpec : Specification<Transaction>, ISingleResultSpecification
{
    public ItemsByDateSpec(int userId, TransactionType transactionType, DateTime startDate, DateTime endDate)
    {
        Query
             .Where(date => date.ExpenseDate.Date >= startDate && date.ExpenseDate.Date <= endDate)
             .Where(h => (transactionType == TransactionType.All) || h.TransactionType == transactionType)
             .Where(h => h.UserId == userId)
             .OrderBy(g => g.ExpenseDate);
    }
}
