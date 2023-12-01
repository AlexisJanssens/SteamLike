using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using ToolBox.DataBase;
using ToolBox.Services;

namespace DAL.Repositories;

public class PriceRepository : Repository, IPriceRepository
{
    public PriceRepository(string connectionString) : base(connectionString)
    {
    }

    public IEnumerable<Price> GetAll()
    {
        throw new NotImplementedException();
    }

    public Price? Get(int id)
    {
        throw new NotImplementedException();
    }

    public Price? Create(Price entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO PriceList OUTPUT inserted.priceId VALUES " +
                              "(@GameId, @UpdateDate, @Price)";
            cmd.Parameters.AddWithValue("GameId", entity.GameId);
            cmd.Parameters.AddWithValue("UpdateDate", entity.UpdateDate);
            cmd.Parameters.AddWithValue("Price", entity.PriceValue);

            entity.PriceId = Convert.ToInt32(DBCommands.CustomScalar(cmd, ConnectionString));

            return entity;

        }
    }

    public bool Update(Price entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Price entity)
    {
        throw new NotImplementedException();
    }
}