
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Models;

public record TransactionIncomeExpenseRecord(
  int incomeId,
  string description,
  decimal amount,
  TransactionDescriptionType transactionDescriptionType,
  DateTime expenseDate,
  int userId,
  TransactionType transactionType
);
