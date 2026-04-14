using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Repositories;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public Task<T> CreateAsync(T entity)
        {
            _context.Add(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            return Task.FromResult(entity);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
