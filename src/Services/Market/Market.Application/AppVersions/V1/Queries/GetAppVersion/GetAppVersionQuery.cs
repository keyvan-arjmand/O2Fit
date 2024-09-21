using Market.Application.Dtos;
using Market.Domain.Enums;

namespace Market.Application.AppVersions.V1.Queries.GetAppVersion;

public record GetAppVersionQuery(MarketType MarketType, string AppVersion):IRequest<AppVersionDto>;