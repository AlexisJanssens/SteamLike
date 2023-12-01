namespace BLL.Models;

public class UserDTO
{
    public int UserID { get; set; }
    public string NickName { get; set; }
    public string Mail { get; set; }
    public double Wallet { get; set; }
    public string? Editor { get; set; }
    public int Role { get; set; }
    public int Status { get; set; }
}