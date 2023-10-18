using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.SharedKernel.Interfaces;


namespace TrackFinance.Web.Endpoints.Historical;

public class GetHistoricalRecordByUser : EndpointBaseAsync
  .WithRequest<GetHistoricalRecordByUserRequest>
  .WithActionResult<GetHistoricalRecordByUserResponse>
{
  private readonly IRepository<Transaction> _repository;
  public const string StoreName = "ebsoldapr-statestore";
  public const string KeyName = "Historial";
  private readonly DaprClient _daprClient;
  private readonly IConfiguration _configuration;

  public GetHistoricalRecordByUser(IRepository<Transaction> repository, DaprClient daprClient, IConfiguration configuration)
  {
    _repository = repository;
    _daprClient = daprClient;
    _configuration = configuration;

  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(GetHistoricalRecordByUserRequest.Route)]
  [SwaggerOperation(
      Summary = "Get Historical Records by user",
      Description = "Get Historical Records by userId",
      OperationId = "HistoricalRecords.GetHistoricalRecordByUser",
      Tags = new[] { "HistoricalRecordsEndpoints" })
  ]
  public override async Task<ActionResult<GetHistoricalRecordByUserResponse>> HandleAsync([FromRoute] GetHistoricalRecordByUserRequest request, CancellationToken cancellationToken = default)
  {
    //Obtencio de datos por cache si usa Dapr o no
    bool useCache = _configuration.GetValue<bool>("UseCache");

    if (useCache)
    {
      var state = await _daprClient.GetStateEntryAsync<GetHistoricalRecordByUserResponse>(StoreName, KeyName);
      if (state.Value == null)
      {
        state.Value = await getHistorical(request, cancellationToken);
        await state.SaveAsync();
      }
      return state.Value;
    }
    else
    {
      return Ok(await getHistorical(request, cancellationToken));
    }
  }

  internal async Task<GetHistoricalRecordByUserResponse> getHistorical(GetHistoricalRecordByUserRequest request, CancellationToken cancellationToken)
  {
    return new GetHistoricalRecordByUserResponse
    {
      HistoricalRecord = (await _repository.ListAsync(cancellationToken))
          .Where(expense => expense.UserId == request.UserId)
          .Where(date => Convert.ToDateTime(date.ExpenseDate.ToString("d")) >= request.StartDate && Convert.ToDateTime(date.ExpenseDate.ToString("d")) <= request.EndDate)
          .Select(expense => new HistoricalRecord(
                                                description: expense.Description,
                                                transactionDescriptionType: expense.TransactionDescriptionType,
                                                amount: expense.Amount,
                                                expenseDate: expense.ExpenseDate,
                                                transactionType: expense.TransactionType))
          .OrderBy(d => d.expenseDate)
          .ToList()
    };
  }
}