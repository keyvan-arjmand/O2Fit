namespace Wallet.Application.Dtos;

public class WalletAmountDto
{
    public string Id { get; set; } = string.Empty;
    public double Amount { get; set; } 
    public bool Adequately { get; set; }
}