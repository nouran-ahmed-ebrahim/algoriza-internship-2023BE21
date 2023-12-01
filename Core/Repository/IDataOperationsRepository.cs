using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IDataOperationsRepository<T>:IBaseRepository<T> where T : class
    {
        T Add(T entity);
    }
}
