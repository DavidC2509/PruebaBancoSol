using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Incomes;

public class List : EndpointBaseAsync
    .WithRequest<IncomeListRequest>
    .WithActionResult<IncomeListResponse>
{
  private readonly IReadRepository<Transaction> _repository;

  public List(IReadRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(IncomeListRequest.Route)]
  [SwaggerOperation(
      Summary = "Gets a list of all Incomes",
      Description = "Gets a list of all Incomes",
      OperationId = "Income.List",
      Tags = new[] { "IncomesEndpoints" })
  ]
  public override async Task<ActionResult<IncomeListResponse>> HandleAsync([FromRoute] IncomeListRequest request, CancellationToken cancellationToken = default)
  {
    var response = new IncomeListResponse();
    response.Incomes = (await _repository.ListAsync(cancellationToken))
        .Where(income => income.UserId == request.UserId && income.TransactionType == TransactionType.Income)
        .Select(income => new IncomeRecord(incomeId: income.Id,
                                           description: income.Description,
                                           amount: income.Amount,
                                           transactionDescriptionType: income.TransactionDescriptionType,
                                           expenseDate: income.ExpenseDate,
                                           userId: income.UserId,
                                           transactionType: income.TransactionType))
        .ToList();

    return Ok(response);
  }
}
