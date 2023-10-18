using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.TransactionAgregate.Specifications;
public class ItemsByWeekSpec : Specification<Transaction>, ISingleResultSpecification
{
  public ItemsByWeekSpec(int userId, TransactionType transactionType)
  {
    var endDate = DateTime.Now;
    var startDate = endDate.AddDays(-7 * 6);
    Query.Where(h => h.ExpenseDate.Date >= startDate.Date && h.ExpenseDate.Date <= endDate.Date)
         .Where(h => h.TransactionType == transactionType || (transactionType == TransactionType.All))
         .Where(h => h.UserId == userId)
         .OrderBy(g => g.ExpenseDate);
  }
}
