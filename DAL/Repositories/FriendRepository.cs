using DAL.Entities;
using DAL.Interfaces;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using ToolBox.DataBase;
using ToolBox.Services;

namespace DAL.Repositories;

public class FriendRepository : Repository, IFriendRepository
{
    public FriendRepository(string connectionString) : base(connectionString)
    {
    }

    public IEnumerable<Friend> GetAll(User user)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM Friend " +
                              "WHERE UserAsker = @userId " +
                              "UNION " +
                              "SELECT * FROM Friend " +
                              "WHERE UserReceiver = @userId";
            cmd.Parameters.AddWithValue("userId", user.UserId);

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToFriend(x));
        }
    }

    public IEnumerable<Friend> GetAll()
    {
        throw new NotImplementedException();
    }

    public Friend? Get(int id)
    {
        throw new NotImplementedException();
    }

    public Friend? GetFriendship(User user1, User user2)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "GET * FROM Friend " +
                              "WHERE UserAsker IN (@UserAskerId, @UserReceiverId) " +
                              "AND UserReceiver IN (@UserAskerId, @UserReceiverId)";
            cmd.Parameters.AddWithValue("UserAskerId", user1.UserId);
            cmd.Parameters.AddWithValue("UserReceiver", user2.UserId);

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToFriend(x)).SingleOrDefault();
        }
    }

    public Friend Create(Friend entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO Friend VALUES " +
                              "(@UserAsker, @UserReceiver, @CreationDate, @Status)";
            cmd.Parameters.AddWithValue("UserAsker", entity.UserAskerId);
            cmd.Parameters.AddWithValue("UserReceiver", entity.UserReceiverId);
            cmd.Parameters.AddWithValue("CreationDate", entity.CreationDate);
            cmd.Parameters.AddWithValue("Status", entity.Status);

            DBCommands.CustomNonQuery(cmd, ConnectionString);

            return entity;

        }
    }

    public bool Update(Friend entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "UPDATE Friend " +
                              "Status = @Status " +
                              "WHERE UserAsker = @UserAsker AND UserReceiver = @UserReceiver";
            cmd.Parameters.AddWithValue("UserAsker", entity.UserAskerId);
            cmd.Parameters.AddWithValue("UserReceiver", entity.UserReceiverId);

            return DBCommands.CustomNonQuery(cmd, ConnectionString) == 1;

        }
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}

   