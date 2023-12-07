namespace BLL.Models;

public class BuyingRecapDTO
{
    public string GameName { get; set; }
    public double Price { get; set; }
    public double NewWalletAmount { get; set; }
    public int UserId { get; set; }
    public int? GiftId { get; set; }
    public DateTime BuyingDate { get; set; }
}