using Dapper;
using System.Data.SqlClient;
using UniFood.Utils;
using UniFood.Models;
using System;


namespace UniFood.Repositories
{
    public static class MenusDAO
    {
        public static async Task<Menu> Get(int id)
        {
            string sqlQuery = "SELECT * FROM [Menu] WHERE Id = @PlaceId";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            Menu result = (await db.QueryAsync<Menu>(sqlQuery, new { id })).First();
            return result;
        }

        public static async Task<Menu> Post(Menu menu)
        {
            menu.Created = DateTime.Now;
            string sqlQuery = "INSERT INTO [Menu] (PlaceId, Category, [Name], Description, Price, ImageUrl, CreatedBy, Created, ModifiedBy, Modified) VALUES (@PlaceId, @Category, @Name, @Description, @Price, @ImageUrl, @CreatedBy, @Created, @ModifiedBy, @Modified)";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<Menu>(sqlQuery, menu);
            return menu;
        }
        public static async Task<bool> Put(Menu menu)
        {
            try
            {
                menu.Modified = DateTime.Now; // Update the 'Modified' timestamp
                string sqlQuery = "UPDATE [Menu] SET PlaceId = @PlaceId, Category = @Category, Name = @Name, Description = @Description, Price = @Price, ImageUrl = @ImageUrl, ModifiedBy = @ModifiedBy, Modified = @Modified WHERE Id = @Id";

                using var db = new SqlConnection(ConfigUtil.ConnectionString);
                int affectedRows = await db.ExecuteAsync(sqlQuery, menu); 
                
                return affectedRows > 0; // Return true if the update was successful
            }
            catch (Exception e)
            {
                // Log the exception details 
                Console.WriteLine(e.Message);
                return false; 
            }
        }
        

        public static async Task<Menu> Delete(int id)
        {
            string sqlQuery = "DELETE FROM [Menu] WHERE Id = @id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<Menu>(sqlQuery, new { id });
            return null;
        }

    }
}
