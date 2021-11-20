using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOZAuthServereSample.Core.Repositories;

namespace OOZAuthServerSample.Data.Repositories
{
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> where Tentity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Tentity> _dbSeet;

        public GenericRepository(AppDbContext context)
        {
            this._context = context;
            _dbSeet = context.Set<Tentity>();
        }

        public async Task AddAsync(Tentity entity)
        {
            await _dbSeet.AddAsync(entity);
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
           return  await _dbSeet.ToListAsync();
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {
            var entity = await _dbSeet.FindAsync(id);

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public void Remove(Tentity entity)
        {
            _dbSeet.Remove(entity);
        }

        public Tentity Update(Tentity entity)
        {
            _context.Entry(entity).State=EntityState.Modified;

            return entity;
        }

        public IQueryable<Tentity> Where(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbSeet.Where(predicate);
        }
    }
}
