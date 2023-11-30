namespace DAL.Entities;

public class GameOfGameList
{
    public int UserId { get; set; }
    public int GameId { get; set; }
    public DateTime Date { get; set; }
    public int PlayinTime { get; set; } = 0;
    public int GiftId { get; set; }
    public int Status { get; set; }
}