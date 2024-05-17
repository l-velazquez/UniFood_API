using Dapper;
using System.Data.SqlClient;
using UniFood.Utils;
using UniFood.Models;
using System;


namespace UniFood.Repositories
{
    public static class UniversitiesDAO
    {

        public static async Task<List<University>> GetAll()
        {
            string sqlQuery = "SELECT * FROM [University]";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            List<University> result = (await db.QueryAsync<University>(sqlQuery)).ToList();
            return result;
        }
        
        public static async Task<University> Get(int id)
        {
            string sqlQuery = "SELECT * FROM [University] WHERE Id = @id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            University result = (await db.QueryAsync<University>(sqlQuery, new { id })).First();
            return result;
        }

        public static async Task<University> Post(University university)
        {
            university.Created = DateTime.Now;
            string sqlQuery = "INSERT INTO [University] ([Name], Address, Description, ImageUrl,Created, CreatedBy) VALUES (@Name, @Address, @Description,@ImageUrl, @Created, @CreatedBy)";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<University>(sqlQuery, university);
            return university;
        }

        public static async Task<bool> Put(University university)
        {
            try
            {
                university.Modified = DateTime.Now; // Update the 'Modified' timestamp

                string sqlQuery = @"
                    UPDATE [University] 
                    SET 
                        Name = @Name, 
                        Address = @Address, 
                        Description = @Description, 
                        ImageUrl = @ImageUrl, 
                        Modified = @Modified, 
                        ModifiedBy = @ModifiedBy, 
                        Created = @Created, 
                        CreatedBy = @CreatedBy 
                    WHERE Id = @Id";

                using var db = new SqlConnection(ConfigUtil.ConnectionString);
                Console.WriteLine($"Executing query: {sqlQuery} with parameters: {Newtonsoft.Json.JsonConvert.SerializeObject(university)}");

                int affectedRows = await db.ExecuteAsync(sqlQuery, university);

                Console.WriteLine($"Rows affected: {affectedRows}");
                
                return affectedRows > 0; // Return true if the update was successful
            }
            catch (SqlException sqlEx)
            {
                // Log SQL-specific exception details
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log general exception details
                Console.WriteLine($"General Error: {ex.Message}");
                return false;
            }
        }


        public static async Task<bool> Delete(int universityId)
        {
            string connectionString = ConfigUtil.ConnectionString;
            using var db = new SqlConnection(connectionString);
            await db.OpenAsync();
            using var transaction = db.BeginTransaction();

            try
            {
                // First, fetch the IDs of all places associated with the university
                string sqlQuery = "SELECT Id FROM [Place] WHERE UniversityId = @id";
                var placeIds = await db.QueryAsync<int>(sqlQuery, new { id = universityId }, transaction);

                // Delete the MenuItems for each place
                foreach (var placeId in placeIds)
                {
                    sqlQuery = "DELETE FROM [Menu] WHERE PlaceId = @placeId";
                    await db.ExecuteAsync(sqlQuery, new { placeId }, transaction);
                }

                // Now delete the Places associated with the University
                sqlQuery = "DELETE FROM [Place] WHERE UniversityId = @id";
                await db.ExecuteAsync(sqlQuery, new { id = universityId }, transaction);

                // Finally, delete the University itself
                sqlQuery = "DELETE FROM [University] WHERE Id = @id";
                int rowsAffected = await db.ExecuteAsync(sqlQuery, new { id = universityId }, transaction);

                // Commit transaction if all deletes are successful
                transaction.Commit();
                
                // Return true if the University was successfully deleted
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Roll back the transaction on error
                transaction.Rollback();
                Console.WriteLine("Error during deletion: " + ex.Message);
                return false; // Consider handling the exception more gracefully
            }
        }


    }
}
