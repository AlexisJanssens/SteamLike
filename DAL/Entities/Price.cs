namespace DAL.Entities;

public class Price
{
    public int PriceId { get; set; }
    public int GameId { get; set; }
    public DateTime UpdateDate { get; set; }
    public double PriceValue { get; set; }
    
    
}