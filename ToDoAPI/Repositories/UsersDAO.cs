using Dapper;
using System.Data.SqlClient;
using UniFood.Models;
using UniFood.Utils;


namespace UniFood.Repositories
{
    public static class UsersDAO
    {
        public static async Task<List<Users>> GetAll()
        {
            string sqlQuery = "SELECT * FROM [User]";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            List<Users> result = (await db.QueryAsync<Users>(sqlQuery)).ToList();
            return result;
        }

        public static async Task<Users> Get(int id)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE Id = @Id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            Users result = (await db.QueryAsync<Users>(sqlQuery, new { id })).First();
            return result;
        }
        public static async Task<Users> GetByName(string name)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE Name = @Name";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            Users result = (await db.QueryAsync<Users>(sqlQuery, new { Name = name })).FirstOrDefault();
            return result;
        }
        public static async Task<Users> GetByEmail(string email)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE Email = @Email";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            Users result = (await db.QueryAsync<Users>(sqlQuery, new { email })).First();
            return result;
        }
        public static async Task<Users> Post(Users user)
        {
            user.RegisteredOn = DateTime.Now;
            user.LastLogin = DateTime.Now;
            string sqlQuery = "INSERT INTO [User] (Role, Email, Password, FirstName, LastName, UniversityId, LastLogin, RegisteredOn) VALUES (@Role, @Email, @Password, @FirstName, @LastName, @UniversityId, @LastLogin, @RegisteredOn); SELECT CAST(SCOPE_IDENTITY() as int)";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<Users>(sqlQuery, user);
            return user;
        }

        public static async Task<Users> Put(Users user)
        {
            string sqlQuery = "UPDATE [User] SET Email = @Email, Password = @Password, Name = @Name, Role = @Role, Created = @Created, CreatedBy = @CreatedBy WHERE Id = @Id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<Users>(sqlQuery, user);
            return user;
        }

        public static async Task<Users> Delete(int id)
        {
            string sqlQuery = "DELETE FROM [User] WHERE Id = @id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<Users>(sqlQuery, new { id });
            return null;
        }
    }
}