using System.Net;
using Ardalis.ApiEndpoints;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.SharedKernel.Interfaces;


namespace TrackFinance.Web.Endpoints.Incomes;

public class Create : EndpointBaseAsync
    .WithRequest<CreateIncomesRequest>
    .WithActionResult<CreateIncomesResponse>
{
  public const string StoreName = "ebsoldapr-statestore";
  public const string KeyName = "Historial";
  private readonly IRepository<Transaction> _repository;
  private readonly DaprClient _daprClient;
  private readonly IConfiguration _configuration;
  public Create(IRepository<Transaction> repository, DaprClient daprClient, IConfiguration configuration)
  {
    _repository = repository;
    _daprClient = daprClient;
    _configuration = configuration;
  }

  [HttpPost("/Incomes")]
  [Produces("application/json")]
  [SwaggerOperation(
    Summary = "Creates a new Incomes",
    Description = "Creates a new Incomes",
    OperationId = "Incomes.Create",
    Tags = new[] { "IncomesEndpoints" })
  ]
  public override async Task<ActionResult<CreateIncomesResponse>> HandleAsync(CreateIncomesRequest request, CancellationToken cancellationToken = default)
  {
    var newExpense = Transaction.CreateIncomes(request.Description,
                                     request.Amount,
                                     request.ExpenseType,
                                     request.ExpenseDate,
                                     request.UserId
                                     );
    var createdItem = await _repository.AddAsync(newExpense, cancellationToken);

    var response = new CreateIncomesResponse
    (
    statusResult: (int)HttpStatusCode.OK,
    expensesId: createdItem.Id
    );

    bool useCache = _configuration.GetValue<bool>("UseCache");

    if (useCache)
    {
      await _daprClient.DeleteStateAsync(StoreName, KeyName);

    }



    return Ok(response);
  }
}
