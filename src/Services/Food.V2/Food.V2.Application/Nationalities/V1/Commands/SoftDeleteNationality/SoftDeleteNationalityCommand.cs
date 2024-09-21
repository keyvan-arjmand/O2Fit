namespace Food.V2.Application.Nationalities.V1.Commands.SoftDeleteNationality;

public class SoftDeleteNationalityCommand:IRequest
{
    public string Id { get; set; } = string.Empty;
}