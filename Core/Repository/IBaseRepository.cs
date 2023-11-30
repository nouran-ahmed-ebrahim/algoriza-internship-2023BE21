﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        //Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAllAsync(int Page, int PageSize);
        T Update(T entity);
        void Delete(int id);
    }
}
