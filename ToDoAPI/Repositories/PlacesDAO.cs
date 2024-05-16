using Dapper;
using System.Data.SqlClient;
using UniFood.Utils;
using UniFood.Models;
using System;


namespace UniFood.Repositories
{
    public static class PlacesDAO
    {
        public static async Task<List<Place>> GetAll()
        {
            string sqlQuery = "SELECT * FROM [Place]";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            List<Place> result = (await db.QueryAsync<Place>(sqlQuery)).ToList();
            return result;
        }
        
        public static async Task<List<Place>> Get(int universityId, int? placeId = null)
        {
            string sqlQuery;
            if (placeId.HasValue)
            {
                sqlQuery = "SELECT * FROM [Place] WHERE UniversityId = @universityId AND Id = @placeId";
                using var db = new SqlConnection(ConfigUtil.ConnectionString);
                var place = await db.QuerySingleOrDefaultAsync<Place>(sqlQuery, new { universityId, placeId });
                return place == null ? new List<Place>() : new List<Place> { place };
            }
            else
            {
                sqlQuery = "SELECT * FROM [Place] WHERE UniversityId = @universityId";
                using var db = new SqlConnection(ConfigUtil.ConnectionString);
                return (await db.QueryAsync<Place>(sqlQuery, new { universityId })).ToList();
            }
        }

        public static async Task<Place> Post(Place place)
        {
            place.Created = DateTime.Now;
            string sqlQuery = "INSERT INTO [Place] ( UniversityId, Name, Address, Schedule, PriceAverage,Description, ImageUrl, CreatedBy, Created, ModifiedBy, Modified) VALUES (@UniversityId, @Name, @Address, @Schedule, @PriceAverage, @Description, @ImageUrl, @CreatedBy, @Created, @ModifiedBy, @Modified)";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<University>(sqlQuery, place);
            return place;
        }

        public static async Task<bool> Put(Place place)
        {
            try
            {
                place.Modified = DateTime.Now; // Update the 'Modified' timestamp
                string sqlQuery = "UPDATE [Place] SET Name = @Name, Description = @Description, CategoryId = @CategoryId, Modified = @Modified, ModifiedBy = @ModifiedBy WHERE Id = @Id";

                using var db = new SqlConnection(ConfigUtil.ConnectionString);
                int affectedRows = await db.ExecuteAsync(sqlQuery, place); 
                
                return affectedRows > 0; // Return true if the update was successful
            }
            catch (Exception e)
            {
                // Log the exception details 
                Console.WriteLine(e.Message);
                return false; 
            }
        }

        public static async Task<bool> Delete(int id)
        {
            string connectionString = ConfigUtil.ConnectionString;
            using var db = new SqlConnection(connectionString);
            db.Open();
            using var transaction = db.BeginTransaction();

            try
            {
                // Delete the Menu items associated with the Place
                string sqlQuery = "DELETE FROM [Menu] WHERE PlaceId = @id";
                await db.ExecuteAsync(sqlQuery, new { id }, transaction);

                // Delete the Place itself
                sqlQuery = "DELETE FROM [Place] WHERE Id = @id";
                int rowsAffected = await db.ExecuteAsync(sqlQuery, new { id }, transaction);

                // Commit transaction if all deletes are successful
                transaction.Commit();

                // Return true if the Place was successfully deleted (check if rowsAffected > 0 for extra safety)
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Roll back the transaction if any error occurs
                transaction.Rollback();
                // Optionally log the exception or handle it further up the stack
                Console.WriteLine("Error during deletion: " + ex.Message);
                return false; // or rethrow the exception depending on your error handling strategy
            }
        }

    }
}
