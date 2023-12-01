namespace BLL.Models;

public class UserForm
{
    public string NickName { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public double Wallet { get; set; }
    public string? Editor { get; set; }
    public int Role { get; set; }
    public int Status { get; set; }
}