﻿using Core.Repository;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Delete(int id)
        {
            T entity = GetById(id);
            if(entity != null)
            {
                return new NotFoundObjectResult($"{id} is not found");
            }
             _context.Set<T>().Remove(entity);
            return new OkObjectResult("Deleted Successfully");
        }

        public T Update(T entity)
        {
            _context.Update(entity); 
            return entity;
        }
    }
}
