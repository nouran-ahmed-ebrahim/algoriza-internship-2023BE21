using Core.Domain;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal class SpecializationRepository : BaseRepository<Specialization>, ISpecializationRepository
    {
        
        public SpecializationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<bool> GetAnyAsync()
        {
            throw new NotImplementedException();
        }
    }
}