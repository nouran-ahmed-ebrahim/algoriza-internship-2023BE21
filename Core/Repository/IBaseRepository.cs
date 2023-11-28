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
        Task<IEnumerable<T>> GetAllAsync(int Page, int PageSize);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
