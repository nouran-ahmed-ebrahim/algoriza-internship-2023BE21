using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IDoctorRepository<T> : ICommonRepository<T> where T : class
    {

    }
}
