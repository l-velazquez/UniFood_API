using UniFood.Models;
using UniFood.Repositories;

namespace UniFood.Services
{
    public class MenusService
    {
        public async Task<Menu> Get(int id)
        {
            Menu menu = await MenusDAO.Get(id);

            return menu;
        }

        public async Task<Menu> Post(Menu menu)
        {
            await MenusDAO.Post(menu);

            return menu;
        }

        public async Task<bool> Put(Menu menu)
        {
            return await MenusDAO.Put(menu);
        }
        
        public async Task<Menu> Delete(int id)
        {
            return await MenusDAO.Delete(id);
        }


    }
}
