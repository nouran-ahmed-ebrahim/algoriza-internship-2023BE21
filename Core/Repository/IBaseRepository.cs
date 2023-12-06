﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IActionResult> Add(T entity);
        T GetById(int id);
        IActionResult GetAll(int? Page, int? PageSize, Expression<Func<T, bool>> criteria = null);

    }
}
