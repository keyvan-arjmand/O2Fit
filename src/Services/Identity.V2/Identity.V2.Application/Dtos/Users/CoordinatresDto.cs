namespace Identity.V2.Application.Dtos.Users;

public class CoordinatesDto : IDto
{
    public CoordinatesDto()
    {
        
    }
    public CoordinatesDto(decimal lat, decimal l)
    {
        Lat = lat;
        Long = l;
    }
    public decimal Lat { get; set; }
    public decimal Long { get; set; }
}