using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class PetRepository : BaseRepository<Pet>, IPetRepository
{
    public PetRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<ICollection<Pet>> GetAllPetsWithInformations()
    {
        return await _context.Pets.Include(x => x.Rescuer).Include(x => x.Adopter).ToArrayAsync();
    }

    public async Task<Pet?> GetPetByName(string name)
    {
        return await _context.Pets.Include(x=>x.Rescuer).FirstOrDefaultAsync(p => p.Name.Equals(name));
    }

}