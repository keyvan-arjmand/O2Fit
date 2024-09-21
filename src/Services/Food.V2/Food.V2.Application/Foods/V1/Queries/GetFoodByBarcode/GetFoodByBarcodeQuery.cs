namespace Food.V2.Application.Foods.V1.Queries.GetFoodByBarcode;

public record GetFoodByBarcodeQuery(string Barcode, string Lang) : IRequest<SearchFoodNameDto>;