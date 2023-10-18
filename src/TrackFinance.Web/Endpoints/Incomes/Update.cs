using Ardalis.ApiEndpoints;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Incomes;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateIncomeRequest>
    .WithActionResult<UpdateIncomesResponse>
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

  [HttpPut(UpdateIncomeRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
     Summary = "Updates a incomes",
     Description = "Updates a incomes",
     OperationId = "Income.Update",
     Tags = new[] { "IncomesEndpoints" })
  ]
  public override async Task<ActionResult<UpdateIncomesResponse>> HandleAsync(UpdateIncomeRequest request, CancellationToken cancellationToken = default)
  {
    var existingIncomes = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingIncomes == null)
    {
      return NotFound();
    }

    existingIncomes.UpdateValue(request.Description, request.Amount, request.ExpenseType, request.ExpenseDate, request.UserId, TransactionType.Income);

    await _repository.UpdateAsync(existingIncomes, cancellationToken);

    var response = new UpdateIncomesResponse(
        expenseRecord: new IncomeRecord(
          existingIncomes.Id,
          existingIncomes.Description,
          existingIncomes.Amount,
          existingIncomes.TransactionDescriptionType,
          existingIncomes.ExpenseDate,
          existingIncomes.UserId,
          existingIncomes.TransactionType));

    bool useCache = _configuration.GetValue<bool>("UseCache");

    if (useCache)
    {
      await _daprClient.DeleteStateAsync(StoreName, KeyName);
    }

    return Ok(response);
  }
}
