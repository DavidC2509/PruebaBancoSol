using Ardalis.ApiEndpoints;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;
using TrackFinance.Web.Endpoints.Models;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateExpenseRequest>
    .WithActionResult<UpdateExpenseResponse>
{
  public const string StoreName = "ebsoldapr-statestore";
  public const string KeyName = "Historial";
  private readonly IRepository<Transaction> _repository;
  private readonly DaprClient _daprClient;
  private readonly IConfiguration _configuration;
  public Update(IRepository<Transaction> repository, DaprClient daprClient, IConfiguration configuration)
  {
    _repository = repository;
    _daprClient = daprClient;
    _configuration = configuration;
  }

  [HttpPut(UpdateExpenseRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
     Summary = "Updates a expense",
     Description = "Updates a expenses",
     OperationId = "Expense.Update",
     Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<UpdateExpenseResponse>> HandleAsync(UpdateExpenseRequest request, CancellationToken cancellationToken = default)
  {
    var existingExpenses = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingExpenses == null)
    {
      return NotFound();
    }

    existingExpenses.UpdateValue(request.Description, request.Amount, request.ExpenseType, request.ExpenseDate, request.UserId, TransactionType.Income);

    await _repository.UpdateAsync(existingExpenses, cancellationToken);

    var response = new UpdateExpenseResponse(
        expenseRecord: new TransactionIncomeExpenseRecord(
          existingExpenses.Id,
          existingExpenses.Description,
          existingExpenses.Amount,
          existingExpenses.TransactionDescriptionType,
          existingExpenses.ExpenseDate,
          existingExpenses.UserId,
          existingExpenses.TransactionType));

    bool useCache = _configuration.GetValue<bool>("UseCache");

    if (useCache)
    {
      await _daprClient.DeleteStateAsync(StoreName, KeyName);

    }
    return Ok(response);
  }
}
