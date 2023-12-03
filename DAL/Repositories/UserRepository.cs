using DAL.Entities;
using DAL.Interfaces;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using ToolBox.DataBase;
using ToolBox.Services;

namespace DAL.Repositories;

public class UserRepository : Repository, IUserRepository
{
    public UserRepository(string connectionString) : base(connectionString)
    {
    }
    
    public IEnumerable<User> GetAll()
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM [User]";

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToUser(x));
        }
    }

    public User? Get(int id)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM [User] WHERE UserId = @id";
            cmd.Parameters.AddWithValue("id", id);

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToUser(x)).SingleOrDefault();
        }
    }

    public User? GetByMail(string mail)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM [User] WHERE Mail = @mail";
            cmd.Parameters.AddWithValue("Mail", mail);

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToUser(x)).SingleOrDefault();
        }
    }

    public User? Create(User entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO [User] OUTPUT inserted.UserId VALUES(" +
                              "@NickName, @Mail, @Password, @Wallet, @EditorName, @Role, @Status" +
                              ")";
            cmd.Parameters.AddWithValue("NickName", entity.NickName);
            cmd.Parameters.AddWithValue("Mail", entity.Email);
            cmd.Parameters.AddWithValue("Password", entity.Password);
            cmd.Parameters.AddWithValue("Wallet", entity.Wallet);
            cmd.Parameters.AddWithValue("EditorName", (object)entity.EditorName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("Role", entity.Role);
            cmd.Parameters.AddWithValue("Status", entity.Status);

            entity.UserId = Convert.ToInt32(DBCommands.CustomScalar(cmd, ConnectionString));
            return entity;
        }
    }

    public bool Update(User entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "UPDATE [User] " +
                              "SET NickName = @NickName, " +
                              "Mail = @Mail, " +
                              "Password = @Password, " +
                              "Wallet = @Wallet, " +
                              "EditorName = @EditorName, " +
                              "Role = @Role, " +
                              "Status = @Status " +
                              "WHERE UserId = @id";
            cmd.Parameters.AddWithValue("NickName", entity.NickName);
            cmd.Parameters.AddWithValue("Mail", entity.Email);
            cmd.Parameters.AddWithValue("Password", entity.Password);
            cmd.Parameters.AddWithValue("Wallet", entity.Wallet);
            cmd.Parameters.AddWithValue("EditorName", entity.EditorName);
            cmd.Parameters.AddWithValue("Role", entity.Role);
            cmd.Parameters.AddWithValue("Status", entity.Status);
            cmd.Parameters.AddWithValue("Id", entity.UserId);

            return DBCommands.CustomNonQuery(cmd, ConnectionString) == 1;
        }
    }

    public bool Delete(int id)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "DELETE FROM [User] " +
                              "WHERE UserId = @Id";
            cmd.Parameters.AddWithValue("Id", id);

            return DBCommands.CustomNonQuery(cmd, ConnectionString) == 1;
        }
    }
}