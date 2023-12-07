namespace BLL.Models;

public class RefundDTO
{
    public string Message { get; set; }
    public string GameName { get; set; }
    public int TimePlayed { get; set; }
    public double AmountRefund { get; set; }
}