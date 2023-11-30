namespace DAL.Entities;

public class Friend
{
    public int UserAskerId { get; set; }
    public int UserReceiverId { get; set; }
    public DateTime CreationDate { get; set; }
    public int Status { get; set; }
}