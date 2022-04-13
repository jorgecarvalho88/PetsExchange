using DataExtension;
using Microsoft.EntityFrameworkCore;

namespace PetApi.Infrastructure.Pet
{
    public class PetRepository : RepositoryBase, IPetRepository
    {
        public PetRepository(PetDbContext context) : base(context)
        {

        }

        public IQueryable<Model.Pet> Get()
        {
            return base.GetQueryable<Model.Pet>();
        }

        private IQueryable<Model.Breed> GetBreed()
        {
            return base.GetQueryable<Model.Breed>();
        }

        public Model.Pet Get(Guid petId)
        {         
            return Get().Include("Breed").Where(p => p.UniqueId == petId).FirstOrDefault();
        }

        public Model.Breed GetBreed(Guid breedId)
        {
            return GetBreed().Where(s => s.UniqueId == breedId).FirstOrDefault();
        }

        public List<Model.Pet> GetByOwner(Guid ownerId)
        {
            return Get().Include("Breed").Where(p => p.Owner == ownerId).ToList();
        }
    }
}
