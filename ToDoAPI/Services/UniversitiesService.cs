using UniFood.Models;
using UniFood.Repositories;

namespace UniFood.Services
{
    public class UniversitiesService
    {
        public async Task<List<University>> GetAll()
        {
            List<University> universities = await UniversitiesDAO.GetAll();

            return universities;
        }

        public async Task<University> Get(int id)
        {
            University university = await UniversitiesDAO.Get(id);

            return university;
        }

        public async Task<University> Post(University university)
        {
            await UniversitiesDAO.Post(university);

            return university;
        }

        public async Task<bool> Put(University university)
        {
            return await UniversitiesDAO.Put(university);
        }

       public async Task<bool> Delete(int id)
        {
            return await UniversitiesDAO.Delete(id);
        }



    }
}
