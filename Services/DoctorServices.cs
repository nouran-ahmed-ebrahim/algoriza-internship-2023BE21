using AutoMapper;
using Core.Repository;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DoctorServices : ApplicationUserService, IDoctorServices
    {
        public DoctorServices(IUnitOfWork UnitOfWork, IMapper mapper) : base(UnitOfWork, mapper)
        {
        }
    }
}
