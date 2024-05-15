using UniFood.Models;
using UniFood.Repositories;

namespace UniFood.Services
{
    public class PlacesService
    {
    
        public async Task<List<Place>> GetAll()
        {
            List<Place> places = await PlacesDAO.GetAll();

            return places;
        }
        public async Task<List<Place>> Get(int id)
        {
            List<Place> places = await PlacesDAO.Get(id);
            return places;
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
