using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface ICommonRepository<T> : IBaseRepository<T> where T : class
    {
        T UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
