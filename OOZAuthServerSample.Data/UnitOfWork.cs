using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOZAuthServereSample.Core.Service.UnitOfWork;

namespace OOZAuthServerSample.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(AppDbContext appContext)
        {
            _context= appContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
