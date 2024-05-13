using Dapper;
using System.Data.SqlClient;
using UniFood.Utils;
using UniFood.Models;
using System;


namespace UniFood.Repositories
{
    public static class FavoritesDAO
    {
        public static async Task<List<Favorite>> GetAll()
        {
            string sqlQuery = "SELECT * FROM Favorites";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            List<Favorite> result = (await db.QueryAsync<Favorite>(sqlQuery)).ToList();
            return result;
        }

        public static async Task<Favorite> Get(int id)
        {
            string sqlQuery = "SELECT * FROM Favorites WHERE Id = @id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            Favorite result = (await db.QueryAsync<Favorite>(sqlQuery, new { id })).First();
            return result;
        }

        public static async Task<Favorite> Post(Favorite favorite)
        {
            string sqlQuery = "INSERT INTO Favorites (UserId, RestaurantId) VALUES (@UserId, @RestaurantId)";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<Favorite>(sqlQuery, favorite);
            return favorite;
        }

        public static async Task<bool> Delete(int id)
        {
            string sqlQuery = "DELETE FROM Favorites WHERE Id = @id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            int affectedRows = await db.ExecuteAsync(sqlQuery, new { id });

            return affectedRows > 0;
        }
    }
}