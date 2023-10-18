using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Incomes;

public class GetIncomeByIdResponse
{
  public GetIncomeByIdResponse(string description, decimal amount, TransactionDescriptionType transactionDescriptionType, DateTime expenseDate)
  {
    Description = description;
    Amount = amount;
    TransactionDescriptionType = transactionDescriptionType;
    ExpenseDate = expenseDate;
  }

  public string Description { get; set; }
  public decimal Amount { get; set; }
  public TransactionDescriptionType TransactionDescriptionType { get; set; }
  public DateTime ExpenseDate { get; set; }
}
