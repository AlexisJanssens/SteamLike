namespace DAL.Entities;

public class FriendOfFriendList
{
    public int UserAskerId { get; set; }
    public string NickName {
        get;
        set;
    }
    public DateTime CreationDate { get; set; }
    public int Status { get; set; }
}