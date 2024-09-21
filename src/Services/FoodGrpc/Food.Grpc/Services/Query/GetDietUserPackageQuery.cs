using MediatR;

namespace Food.Grpc.Services.Query;

public class GetDietUserPackageQuery:IRequest<ResonseDietPack>
{
    public RequestGetDietPack DietPacks { get; set; }
}