using Ardalis.ApiEndpoints;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate.Specifications;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteExpenseRequest>
    .WithoutResult
{
  public const string StoreName = "ebsoldapr-statestore";
  public const string KeyName = "Historial";
  private readonly IRepository<Transaction> _repository;
  private readonly DaprClient _daprClient;
  private readonly IConfiguration _configuration;
  public Delete(IRepository<Transaction> repository, DaprClient daprClient, IConfiguration configuration)
  {
    _repository = repository;
    _daprClient = daprClient;
    _configuration = configuration;
  }

  [HttpDelete(DeleteExpenseRequest.Route)]
  [SwaggerOperation(
      Summary = "Deletes a Expenses",
      Description = "Delete a Expenses Saved",
      OperationId = "Expenses.Delete",
      Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync([FromRoute] DeleteExpenseRequest request, CancellationToken cancellationToken = default)
  {
    var transaction = new TransactionById(request.ExpenseId, TransactionType.Expense);
    var aggregateToDelete = await _repository.GetBySpecAsync(transaction, cancellationToken);
    if (aggregateToDelete == null) return NotFound();

    await _repository.DeleteAsync(aggregateToDelete, cancellationToken);

    bool useCache = _configuration.GetValue<bool>("UseCache");

    if (useCache)
    {
      await _daprClient.DeleteStateAsync(StoreName, KeyName);

    }
    return NoContent();
  }
}
