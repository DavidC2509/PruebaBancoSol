using System.Net;
using Ardalis.ApiEndpoints;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.SharedKernel.Interfaces;


namespace TrackFinance.Web.Endpoints.Expenses;

public class Create : EndpointBaseAsync
    .WithRequest<CreateExpensesRequest>
    .WithActionResult<CreatExpenseResponse>
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

  [HttpPost(CreateExpensesRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
    Summary = "Creates a new Expenses",
    Description = "Creates a new Expenses",
    OperationId = "Expenses.Create",
    Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<CreatExpenseResponse>> HandleAsync(CreateExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var newExpense = Transaction.CreateExpenses(request.Description,
                                     request.Amount,
                                     request.ExpenseType,
                                     request.ExpenseDate,
                                     request.UserId
                                     );
    var createdItem = await _repository.AddAsync(newExpense, cancellationToken);

    var response = new CreatExpenseResponse
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
