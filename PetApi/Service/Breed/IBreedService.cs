using PetApiContract;

public interface IBreedService
{
    BreedContract Get(Guid uniqueId);
    BreedContract Get(string name);
    BreedContract Add(BreedContract breed);
    //BreedContract Update(BreedContract breed);
    //BreedContract Delete(Guid uniqueId);
}

