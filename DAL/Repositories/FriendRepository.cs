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

    public Friend? Create(Friend entity)
    {
        throw new NotImplementedException();
    }

    public bool Update(Friend entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Friend entity)
    {
        throw new NotImplementedException();
    }
}