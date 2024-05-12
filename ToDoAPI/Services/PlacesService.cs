using UniFood.Models;
using UniFood.Repositories;

namespace UniFood.Services
{
    public class PlacesService
    {
        public async Task<Place> Get(int id)
        {
            Place place = await PlacesDAO.Get(id);

            return place;
        }

        public async Task<Place> Post(Place place)
        {
            await PlacesDAO.Post(place);

            return place;
        }

        public async Task<bool> Put(Place place)
        {
            return await PlacesDAO.Put(place);
        }

        public async Task<bool> Delete(int id)
        {
            return await PlacesDAO.Delete(id);
        }


    }
}
