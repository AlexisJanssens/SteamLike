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
            cmd.Parameters.AddWithValue("Id", entity.GameId);

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

    public bool UpdateGameList(GameOfGameList game)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "UPDATE GameList SET " +
                              "UserId = @userId, " +
                              "GameId = @gameId," +
                              "BuyingDate = @buyingDate," +
                              "PlayingTime = @playingTime," +
                              "GiftId = @giftId," +
                              "Status = @status " +
                              "WHERE UserId = @userId AND GameId = @gameId";
            cmd.Parameters.AddWithValue("userId", game.UserId);
            cmd.Parameters.AddWithValue("gameId", game.GameId);
            cmd.Parameters.AddWithValue("buyingDate", game.Date);
            cmd.Parameters.AddWithValue("playingTime", game.PlayinTime);
            cmd.Parameters.AddWithValue("giftId", (object)game.GiftId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("status", game.Status);

            return DBCommands.CustomNonQuery(cmd, ConnectionString) == 1;

        }
    }

    public IEnumerable<GameOfLibrary> GetMyGames(int userId)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText =
                "SELECT G.Name, G.GameId, Status FROM GameList JOIN dbo.Game G on G.GameId = GameList.GameId WHERE UserId = @userId AND Status = 1";
            cmd.Parameters.AddWithValue("UserId", userId);

            return DBCommands.CustomReader(cmd, ConnectionString, x => DbMapper.ToGameOfLibrary(x));
        }
    }

    public IEnumerable<SoldGame> GetAllSales(int devId)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT G.Name, GL.GameId, G.DevId, GL.BuyingDate, ( " +
                              "SELECT TOP 1 PL.Price FROM Pricelist PL " +
                              "WHERE PL.GameId = GL.GameId AND PL.UpdateDate <= GL.BuyingDate " +
                              "ORDER BY PL.UpdateDate DESC ) AS BuyingPrice " +
                              "FROM GameList GL " +
                              "JOIN GAme G ON GL.GameId = G.GameId " +
                              "WHERE G.DevId = @devId " +
                              "ORDER BY GL.GameId, GL.BuyingDate";

            cmd.Parameters.AddWithValue("DevId", devId);
            return DBCommands.CustomReader(cmd, ConnectionString, reader => DbMapper.ToSoldGame(reader));
        }
    }

    public bool AddWhish(GameOfGameList game)
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

    public bool EnterInGame(int userId, int gameId)
    {
        using (SqlCommand sqlCommand = new SqlCommand())
        {
            sqlCommand.CommandText = " UPDATE GameList SET Status = 6 WHERE UserId = @userId AND GameId = @gameId";
            sqlCommand.Parameters.AddWithValue("userId", userId);
            sqlCommand.Parameters.AddWithValue("gameId", gameId);

            return DBCommands.CustomNonQuery(sqlCommand, ConnectionString) == 1;


        }
    }

    public bool QuitInGame(int userId, int gameId)
    {
        using (SqlCommand sqlCommand = new SqlCommand())
        {
            sqlCommand.CommandText = " UPDATE GameList SET Status = 1 WHERE UserId = @userId AND GameId = @gameId";
            sqlCommand.Parameters.AddWithValue("userId", userId);
            sqlCommand.Parameters.AddWithValue("gameId", gameId);

            return DBCommands.CustomNonQuery(sqlCommand, ConnectionString) == 1;


        }
    }

    public bool AddGameTime(int userId, int gameId)
    {
        using (SqlCommand sqlCommand = new SqlCommand())
        {
            sqlCommand.CommandText = " UPDATE GameList SET PlayingTime += 1 WHERE UserId = @userId AND GameId = @gameId";
            sqlCommand.Parameters.AddWithValue("userId", userId);
            sqlCommand.Parameters.AddWithValue("gameId", gameId);

            return DBCommands.CustomNonQuery(sqlCommand, ConnectionString) == 1;
        }
    }
}