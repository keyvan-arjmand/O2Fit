namespace Advertise.Application.AdminAdvertises.V1.Queries.GetAllActiveAdminAdvertise;

public record GetAllActiveAdminAdvertiseQuery(Language Language) : IRequest<List<AdminAdvertiseDto>>;