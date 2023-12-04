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

    public IEnumerable<FriendOfFriendList> GetAll(User user)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT UserAsker as UserId, [user].nickname, creationDate, friend.status FROM Friend " +
                              "JOIN [user] ON Friend.UserAsker = [user].UserId " +
                              "WHERE UserReceiver = @userId " +
                              "UNION " +
                              "SELECT UserReceiver, [user].nickname, creationDate, friend.status FROM Friend " +
                              "JOIN [user] ON Friend.UserAsker = [user].UserId " +
                              "WHERE UserAsker = @userId";
            cmd.Parameters.AddWithValue("userId", user.UserId);

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToFriendOfFriendList(x));
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
            cmd.CommandText = "SELECT * FROM Friend " +
                              "WHERE UserAsker IN (@UserAskerId, @UserReceiverId) " +
                              "AND UserReceiver IN (@UserAskerId, @UserReceiverId)";
            cmd.Parameters.AddWithValue("UserAskerId", user1.UserId);
            cmd.Parameters.AddWithValue("UserReceiverId", user2.UserId);

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
            cmd.CommandText = "UPDATE Friend Set " +
                              "Status = @Status " +
                              "WHERE UserAsker = @UserAsker AND UserReceiver = @UserReceiver";
            cmd.Parameters.AddWithValue("UserAsker", entity.UserAskerId);
            cmd.Parameters.AddWithValue("UserReceiver", entity.UserReceiverId);
            cmd.Parameters.AddWithValue("Status", entity.Status);

            return DBCommands.CustomNonQuery(cmd, ConnectionString) == 1;

        }
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id1, int id2)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "DELETE FROM Friend " +
                              "WHERE UserAsker IN (@id1, @id2) " +
                              "AND UserReceiver IN (@id1, @id2)";
            cmd.Parameters.AddWithValue("id1", id1);
            cmd.Parameters.AddWithValue("id2", id2);

            return DBCommands.CustomNonQuery(cmd, ConnectionString) == 1;
        }
    }
}

   