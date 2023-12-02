using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface ICommonRepository<T> : IBaseRepository<T> where T : class
    {
        IActionResult Update(T entity);
        IActionResult Add(T entity);

    }
}
