using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DataOperationsRepository<T> : CommonRepository<T>, IDataOperationsRepository<T> where T : class
    {
        public DataOperationsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }
    }
}
