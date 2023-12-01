using DAL.Entities;
using Microsoft.Data.SqlClient;

namespace DAL.Mappers;

public static class DbMapper
{
    public static User ToUser(this SqlDataReader reader)
    {
        return new User()
        {
            UserId = (int)reader["UserId"],
            NickName = reader["NickName"].ToString(),
            Email = reader["mail"].ToString(),
            Password = reader["Password"].ToString(),
            Wallet = Convert.ToDouble(reader["Wallet"]),
            EditorName = reader["Wallet"] == DBNull.Value ? null : reader["Wallet"].ToString(),
            Role = (int)reader["Role"],
            Status = (int)reader["Status"]
        };
    }

    public static Friend ToFriend(this SqlDataReader reader)
    {
        return new Friend()
        {
            UserAskerId = (int)reader["UserAsker"],
            UserReceiverId = (int)reader["UserReceiver"],
            CreationDate = Convert.ToDateTime(reader["CreationDate"]),
            Status = (int)reader["Status"]
        };
    }

    public static Game ToGame(this SqlDataReader reader)
    {
        return new Game()
        {
            GameId = (int)reader["GameId"],
            Name = reader["Name"].ToString(),
            DevId = (int)reader["DevId"],
            Version = reader["Version"].ToString()
        };
    }

    public static GameOfGameList ToGameOfGameList(this SqlDataReader reader)
    {
        return new GameOfGameList()
        {
            UserId = (int)reader["UserId"],
            GameId = (int)reader["GameId"],
            Date = Convert.ToDateTime(reader["BuyingDate"]),
            PlayinTime = (int)reader["PlayingTime"],
            GiftId = (int)reader["GiftId"],
            Status = (int)reader["Status"]
        };
    }

    public static Price ToPrice(this SqlDataReader reader)
    {
        return new Price()
        {
            PriceId = (int)reader["PriceId"],
            GameId = (int)reader["GameId"],
            UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
            PriceValue = Convert.ToDouble(reader["Price"])
        };
    }
}