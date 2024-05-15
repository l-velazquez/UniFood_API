using Dapper;
using System.Data.SqlClient;
using UniFood.Models;
using UniFood.Utils;


namespace UniFood.Repositories
{
    public static class UsersDAO
    {
        public static async Task<List<User>> GetAll()
        {
            string sqlQuery = "SELECT * FROM [User]";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            List<User> result = (await db.QueryAsync<User>(sqlQuery)).ToList();
            return result;
        }

        public static async Task<User> Get(int id)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE Id = @Id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            User result = (await db.QueryAsync<User>(sqlQuery, new { id })).First();
            return result;
        }
        public static async Task<User> GetByName(string name)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE Name = @Name";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            User result = (await db.QueryAsync<User>(sqlQuery, new { Name = name })).FirstOrDefault();
            return result;
        }
        public static async Task<User> GetByEmail(string email)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE Email = @Email";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            User result = (await db.QueryAsync<User>(sqlQuery, new { email })).First();
            return result;
        }
        public static async Task<User> Post(User user)
        {
            user.RegisteredOn = DateTime.Now;
            user.LastLogin = DateTime.Now;
            string sqlQuery = @"
                INSERT INTO [User] (Role, Email, Password, FirstName, LastName, UniversityId, LastLogin, RegisteredOn)
                VALUES (@Role, @Email, @Password, @FirstName, @LastName, @UniversityId, @LastLogin, @RegisteredOn);
                SELECT CAST(SCOPE_IDENTITY() as int)";
                
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            int newId = await db.ExecuteScalarAsync<int>(sqlQuery, new
            {
                user.Role,
                user.Email,
                user.Password,
                user.FirstName,
                user.LastName,
                user.UniversityId,
                user.LastLogin,
                user.RegisteredOn
            });
            user.Id = newId;
            return user;
        }

        public static async Task<User> Put(User user)
        {
            string sqlQuery = "UPDATE [User] SET Email = @Email, Password = @Password, Name = @Name, Role = @Role, Created = @Created, CreatedBy = @CreatedBy WHERE Id = @Id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<User>(sqlQuery, user);
            return user;
        }

        public static async Task<User> Delete(int id)
        {
            string sqlQuery = "DELETE FROM [User] WHERE Id = @id";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.QueryAsync<User>(sqlQuery, new { id });
            return null;
        }
    }
}