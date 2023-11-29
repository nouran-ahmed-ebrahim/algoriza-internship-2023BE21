using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class PatientRepository : BaseRepository<ApplicationUser>, IPatientsRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {

        }
        public Task<ApplicationUser> AddAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllAsync(int Page, int PageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> NumOfPatients()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> UpdateAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
