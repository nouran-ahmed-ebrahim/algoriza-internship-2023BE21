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

        public Specialization GetByName(string Specialization)
        {
            return _context.Specializations.FirstOrDefault(s => s.Name == Specialization);
        }

    }
}