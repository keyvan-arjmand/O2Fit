namespace Food.V2.Application.Foods.V1.Queries.CheckDuplicateBarCode;

public record CheckDuplicateBarCodeQuery(string BarcodeGs1 , string BarcodeNational): IRequest<bool>;