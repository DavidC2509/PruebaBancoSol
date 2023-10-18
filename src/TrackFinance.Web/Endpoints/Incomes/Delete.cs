using Ardalis.ApiEndpoints;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate.Specifications;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Incomes;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteIncomeRequest>
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

  [HttpDelete(DeleteIncomeRequest.Route)]
  [SwaggerOperation(
      Summary = "Deletes a Incomes",
      Description = "Delete a Income Saved",
      OperationId = "Income.Delete",
      Tags = new[] { "IncomesEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync([FromRoute] DeleteIncomeRequest request, CancellationToken cancellationToken = default)
  {
    var transaction = new TransactionById(request.IncomeId, TransactionType.Income);
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
