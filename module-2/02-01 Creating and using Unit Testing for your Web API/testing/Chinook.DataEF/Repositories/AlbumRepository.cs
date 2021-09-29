using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ChinookContext _context;

        public AlbumRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> AlbumExists(int id) =>
            await _context.Albums.AnyAsync(a => a.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<Album>> GetAll() => await _context.Albums.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<Album> GetById(int? id)
        {
            var dbAlbum = await _context.Albums.FindAsync(id);
            return dbAlbum;
        }

        public async Task<Album> Add(Album newAlbum)
        {
            await _context.Albums.AddAsync(newAlbum);
            await _context.SaveChangesAsync();
            return newAlbum;
        }

        public async Task<bool> Update(Album album)
        {
            if (!await AlbumExists(album.Id))
                return false;
            _context.Albums.Update(album);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await AlbumExists(id))
                return false;
            var toRemove = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Album>> GetByArtistId(int id) =>
            await _context.Albums.Where(a => a.ArtistId == id).AsNoTrackingWithIdentityResolution().ToListAsync();
    }
}