using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context) {
            _context = context;
        }

        public virtual async Task<IActionResult> Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                return new OkObjectResult(entity);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while adding: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
        }
        public virtual IActionResult GetAll(int? Page, int? PageSize, 
                        Expression<Func<T,bool>> criteria = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                
                if (criteria != null)
                    query.Where(criteria);

                if (Page.HasValue)
                    query = query.Skip((Page.Value - 1) * PageSize.Value);

                if (PageSize.HasValue)
                    query = query.Take(PageSize.Value);

                return new OkObjectResult(query);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"There is a problem during getting the data {ex.Message}")
                {
                    StatusCode=500
                };
            }
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
