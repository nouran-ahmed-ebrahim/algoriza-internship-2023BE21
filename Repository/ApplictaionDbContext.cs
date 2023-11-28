using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplictaionDbContext: DbContext
    {
        public ApplictaionDbContext(DbContextOptions<ApplictaionDbContext> options) : base(options)
        {
        }
    }
}
