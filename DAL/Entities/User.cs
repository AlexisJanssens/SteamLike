namespace DAL.Entities;

public class User
{
    public int UserId { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public double Wallet { get; set; }
    public string EditorName { get; set; }
    public int Role { get; set; }
    public int Status { get; set; }
}
