using UniFood.Models;
using UniFood.Repositories;
using UniFood.Utils;

namespace UniFood.Services
{
    public class LoginService
    {
        public async Task<string> LoginAsync(Login login)
        {
            Users user = await UsersDAO.GetByEmail(login.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.Password != login.Password)
            {
                throw new Exception("Invalid password");
            }
            // Add the return statement here
            return JWTUtil.GenerateJWT(user);
        }
    }
}
