using Core.Domain;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal class SpecializationRepository : BaseRepository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(ApplictationDbContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(List<Specialization> Specializations)
        {
            _context.Specializations.AddRange(Specializations);

            //_context.SaveChanges();
            //return Task.CompletedTask;
        }

        public async Task<bool> GetAnyAsync()
        {
            return  await _context.Specializations.AnyAsync();
        }
    }
}