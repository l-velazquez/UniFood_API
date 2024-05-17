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
            string sqlQuery = "SELECT * FROM UserFavoritePlace";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            List<Favorite> result = (await db.QueryAsync<Favorite>(sqlQuery)).ToList();
            return result;
        }

        public static async Task<List<Favorite>> Get(int id)
        {
            string sqlQuery = "SELECT * FROM UserFavoritePlace WHERE UserId = @id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            List<Favorite> result = (await db.QueryAsync<Favorite>(sqlQuery, new { id })).ToList();
            return result;
        }

        public static async Task<Favorite> Post(Favorite favorite)
        {
            string checkPlaceQuery = "SELECT COUNT(*) FROM Place WHERE Id = @PlaceId";
            string insertQuery = "INSERT INTO UserFavoritePlace (UserId, PlaceId) VALUES (@UserId, @PlaceId)";

            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            
            // Check if PlaceId exists
            var placeExists = await db.ExecuteScalarAsync<int>(checkPlaceQuery, new { PlaceId = favorite.PlaceId });

            if (placeExists == 0)
            {
                throw new Exception($"Place with Id {favorite.PlaceId} does not exist.");
            }

            // Insert if PlaceId exists
            await db.ExecuteAsync(insertQuery, favorite);
            return favorite;
        }


        public static async Task<bool> Delete(int id)
        {
            string sqlQuery = "DELETE FROM UserFavoritePlace WHERE Id = @id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            int affectedRows = await db.ExecuteAsync(sqlQuery, new { id });

            return affectedRows > 0;
        }
    }
}