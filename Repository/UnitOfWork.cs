using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private ApplictaionDbContext _context;

        //public IBaseRepository<Author> Authors { get; private set; }
        //public IBooksRepository Books { get; private set; }
        public UnitOfWork(ApplictaionDbContext context) {
            _context = context;

            //Authors = new BaseRepository<Author>(_context);
            //Books = new BooksRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
