using DAL.Entities;
using DAL.Interfaces;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using ToolBox.DataBase;
using ToolBox.Services;

namespace DAL.Repositories;

public class GameRepository : Repository, IGameRepository
{
    public GameRepository(string connectionString) : base(connectionString)
    {
    }

    public IEnumerable<Game> GetAll()
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM Game";

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToGame(x));
        }
    }

    public Game? Get(int id)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM [Game] WHERE GameId = @id";
            cmd.Parameters.AddWithValue("id", id);

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToGame(x)).SingleOrDefault();
        }
    }

    public Game Create(Game entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO Game OUTPUT inserted.GameId VALUES (@Name, @Dev, @Version)";
            cmd.Parameters.AddWithValue("Name", entity.Name);
            cmd.Parameters.AddWithValue("Dev", entity.DevId);
            cmd.Parameters.AddWithValue("Version", entity.Version);

            entity.GameId = Convert.ToInt32(DBCommands.CustomScalar(cmd, ConnectionString));

            return entity;

        }
    }

    public bool Update(Game entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "UPDATE Game SET " +
                              "Name = @Name, " +
                              "DevId = @Dev, " +
                              "Version = @Version " +
                              "WHERE GameId = @id ";
            cmd.Parameters.AddWithValue("Name", entity.Name);
            cmd.Parameters.AddWithValue("Dev", entity.DevId);
            cmd.Parameters.AddWithValue("Version", entity.Version);
            cmd.Parameters.AddWithValue("GameId", entity.GameId);

            return DBCommands.CustomNonQuery(cmd, ConnectionString) == 1;
        }
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool BuyGame(GameOfGameList game)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO GameList VALUES (" +
                              "@UserID ,@GameId, @BuyingDate, @PlayingTime, @GiftId, @Status" +
                              ")";
            cmd.Parameters.AddWithValue("UserId", game.UserId);
            cmd.Parameters.AddWithValue("GameId", game.GameId);
            cmd.Parameters.AddWithValue("BuyingDate", game.Date);
            cmd.Parameters.AddWithValue("PlayingTime", game.PlayinTime);
            cmd.Parameters.AddWithValue("GiftId", (object)game.GiftId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("Status", game.Status);

           return DBCommands.CustomNonQuery(cmd, ConnectionString) == 1;
        }
    }

    public GameOfGameList? GetById(int gameId, int userId)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM GameList WHERE GameId = @gameId AND UserId = @userId";
            cmd.Parameters.AddWithValue("gameId", gameId);
            cmd.Parameters.AddWithValue("userId", userId);

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToGameOfGameList(x)).SingleOrDefault();

        }
    }
}