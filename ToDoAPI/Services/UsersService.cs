using UniFood.Models;
using UniFood.Repositories;

namespace UniFood.Services
{
    public class UserService
    {
        public async Task<List<Users>> GetAll()
        {
            List<Users> users = await UsersDAO.GetAll();

            return users;
        }

        public async Task<List<Users>> GetPaginated(int pageNumber, int pageSize)
        {
            return await UsersDAO.GetPaginated(pageNumber, pageSize);
        }

        public async Task<Users> GetByEmail(string email)
        {
            Users user = await UsersDAO.GetByEmail(email);

            return user;
        }

        public async Task<Users> Get(int id)
        {
            Users user = await UsersDAO.Get(id);

            return user;
        }

        public async Task<Users> Post(Users user)
        {
            await UsersDAO.Post(user);

            return user;
        }

        public async Task<Users> Put(Users user)
        {
            return await UsersDAO.Put(user);
        }

        public async Task<Users> Delete(int id)
        {
            return await UsersDAO.Delete(id);
        }
    }
}