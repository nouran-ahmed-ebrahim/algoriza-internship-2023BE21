using Core.Repository;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Delete(int id)
        {
            try
            {
                T entity = GetById(id);
                if (entity == null)
                {
                    return new NotFoundObjectResult($"{id} is not found");
                }
                _context.Set<T>().Remove(entity);
                return new OkObjectResult("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred while adding: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
        }
    }
}
