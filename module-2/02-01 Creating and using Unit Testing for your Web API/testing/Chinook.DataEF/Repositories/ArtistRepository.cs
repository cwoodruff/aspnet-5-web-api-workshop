using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ChinookContext _context;

        public ArtistRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> ArtistExists(int id) =>
            await _context.Artists.AnyAsync(a => a.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<Artist>> GetAll() =>
            await _context.Artists.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<Artist> GetById(int id) =>
            await _context.Artists.FindAsync(id);

        public async Task<Artist> Add(Artist newArtist)
        {
            await _context.Artists.AddAsync(newArtist);
            await _context.SaveChangesAsync();
            return newArtist;
        }

        public async Task<bool> Update(Artist artist)
        {
            if (!await ArtistExists(artist.Id))
                return false;
            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await ArtistExists(id))
                return false;
            var toRemove = await _context.Artists.FindAsync(id);
            _context.Artists.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}