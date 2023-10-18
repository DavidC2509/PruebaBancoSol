using Ardalis.Specification;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.TransactionAgregate.Specifications;

public class TransactionById : Specification<Transaction>, ISingleResultSpecification
{
  public TransactionById(int expenseId, TransactionType transactionType)
  {
    Query
        .Where(project => project.Id == expenseId
                && project.TransactionType == transactionType);
  }
}
