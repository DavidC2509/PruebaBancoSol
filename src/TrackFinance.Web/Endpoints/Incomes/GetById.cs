using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate.Specifications;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Incomes;

public class GetById : EndpointBaseAsync
    .WithRequest<GetIncomeByIdRequest>
    .WithActionResult<GetIncomeByIdResponse>
{
  private readonly IRepository<Transaction> _repository;

  public GetById(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(GetIncomeByIdRequest.Route)]
  [SwaggerOperation(
      Summary = "Gets a single Income",
      Description = "Gets a single Income by Id",
      OperationId = "Incomes.GetById",
      Tags = new[] { "IncomesEndpoints" })
  ]
  public override async Task<ActionResult<GetIncomeByIdResponse>> HandleAsync([FromRoute] GetIncomeByIdRequest request,
      CancellationToken cancellationToken)
  {
    var transaction = new TransactionById(request.IncomeId, TransactionType.Income);
    var entity = await _repository.GetBySpecAsync(transaction, cancellationToken);
    if (entity == null) return NotFound();

    var response = new GetIncomeByIdResponse
    (
        description: entity.Description,
        amount: entity.Amount,
        transactionDescriptionType: entity.TransactionDescriptionType,
        expenseDate: entity.ExpenseDate
    );
    return Ok(response);
  }
}
