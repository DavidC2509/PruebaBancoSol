using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Incomes;

public record IncomeRecord(
  int incomeId,
  string description,
  decimal amount,
  TransactionDescriptionType transactionDescriptionType,
  DateTime expenseDate,
  int userId,
  TransactionType transactionType
);
