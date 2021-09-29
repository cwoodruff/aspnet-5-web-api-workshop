using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ChinookContext _context;

        public GenreRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> GenreExists(int id) =>
            await _context.Genres.AnyAsync(g => g.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<Genre>> GetAll() =>
            await _context.Genres.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<Genre> GetById(int id)
        {
            var dbGenre = await _context.Genres.FindAsync(id);
            return dbGenre;
        }

        public async Task<Genre> Add(Genre newGenre)
        {
            await _context.Genres.AddAsync(newGenre);
            await _context.SaveChangesAsync();
            return newGenre;
        }

        public async Task<bool> Update(Genre genre)
        {
            if (!await GenreExists(genre.Id))
                return false;
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await GenreExists(id))
                return false;
            var toRemove = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}