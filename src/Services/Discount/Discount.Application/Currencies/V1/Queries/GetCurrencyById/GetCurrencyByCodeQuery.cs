using Discount.Application.Dtos;
using Discount.Domain.Aggregates.CurrencyAggregate;

namespace Discount.Application.Currencies.V1.Queries.GetCurrencyById;

public record GetCurrencyByCodeQuery(string Code):IRequest<Currency>;