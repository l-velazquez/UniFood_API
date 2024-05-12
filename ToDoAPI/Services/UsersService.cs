using UniFood.Models;
using UniFood.Repositories;

namespace UniFood.Services
{
    public class UserService
    {
        public async Task<List<User>> GetAll()
        {
            List<User> users = await UsersDAO.GetAll();

            return users;
        }

        public async Task<User> Get(int id)
        {
            User user = await UsersDAO.Get(id);

            return user;
        }

        public async Task<User> Post(User user)
        {
            await UsersDAO.Post(user);

            return user;
        }

        public async Task<User> Put(User user)
        {
            return await UsersDAO.Put(user);
        }

        public async Task<User> Delete(int id)
        {
            return await UsersDAO.Delete(id);
        }
    }
}