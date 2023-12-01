using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CommonRepository<T> : BaseRepository<T>, ICommonRepository<T> where T : class
    {
        public CommonRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
        }

        public T Update(T entity)
        {
            _context.Update(entity); 
            return entity;
        }
    }
}
