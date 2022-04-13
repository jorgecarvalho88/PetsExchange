using DataExtension;
using Microsoft.EntityFrameworkCore;

namespace PetApi.Infrastructure.Breed
{
    public class BreedRepository : RepositoryBase, IBreedRepository
    {
        public BreedRepository(PetDbContext context) : base(context)
        {

        }

        public IQueryable<Model.Breed> Get()
        {
            return base.GetQueryable<Model.Breed>();
        }

        private IQueryable<Model.Type> GetType()
        {
            return base.GetQueryable<Model.Type>();
        }

        public Model.Breed Get(Guid breedId)
        {
            return Get().Include("PetType").Where(b => b.UniqueId == breedId).FirstOrDefault();
        }

        public Model.Breed Get(string breedName)
        {
            return Get().Include("PetType").Where(b => b.Name == breedName).FirstOrDefault();
        }

        public Model.Type GetType(Guid typeId)
        {
            return GetType().Where(b => b.UniqueId == typeId).FirstOrDefault();
        }
    }
}
