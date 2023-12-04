using Core.Domain;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SpecializationRepository(ApplicationDbContext context) : BaseRepository<Specialization>(context), ISpecializationRepository
    {
        public Specialization GetByName(string Specialization)
        {
            return _context.Specializations.FirstOrDefault(s => s.Name == Specialization);
        }
    }
}