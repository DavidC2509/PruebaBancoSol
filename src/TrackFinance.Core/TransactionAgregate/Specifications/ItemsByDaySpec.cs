using Ardalis.Specification;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.TransactionAgregate.Specifications;
public class ItemsByDaySpec : Specification<Transaction>, ISingleResultSpecification
{
  public ItemsByDaySpec(int userId, TransactionType transactionType) 
  { 
    var endDate = DateTime.Now;
    var startDate = endDate.AddDays(-7);
    Query.Where(h => h.ExpenseDate.Date >= startDate.Date && h.ExpenseDate.Date <= endDate.Date)
         .Where(h => (transactionType == TransactionType.All) || h.TransactionType == transactionType )
         .Where(h => h.UserId == userId)
         .OrderBy(g => g.ExpenseDate);
  }
}
