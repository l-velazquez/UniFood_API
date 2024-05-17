using UniFood.Models;
using UniFood.Repositories;

namespace UniFood.Services
{
    public class FavoritesService
    {
        public async Task<List<Favorite>> GetAll()
        {
            List<Favorite> favorites = await FavoritesDAO.GetAll();

            return favorites;
        }

        public async Task<List<Favorite>> Get(int id)
        {
            List<Favorite>favorite = await FavoritesDAO.Get(id);

            return favorite;
        }

        public async Task<Favorite> Post(Favorite favorite)
        {
            await FavoritesDAO.Post(favorite);

            return favorite;
        }

        public async Task<bool> Delete(int id)
        {
            return await FavoritesDAO.Delete(id);
        }
    }
}