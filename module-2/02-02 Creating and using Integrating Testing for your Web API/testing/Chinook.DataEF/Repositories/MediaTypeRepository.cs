using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly ChinookContext _context;

        public MediaTypeRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> MediaTypeExists(int id) =>
            await _context.MediaTypes.AnyAsync(i => i.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<MediaType>> GetAll() =>
            await _context.MediaTypes.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<MediaType> GetById(int id) =>
            await _context.MediaTypes.FindAsync(id);

        public async Task<MediaType> Add(MediaType newMediaType)
        {
            await _context.MediaTypes.AddAsync(newMediaType);
            await _context.SaveChangesAsync();
            return newMediaType;
        }

        public async Task<bool> Update(MediaType mediaType)
        {
            if (!await MediaTypeExists(mediaType.Id))
                return false;
            _context.MediaTypes.Update(mediaType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await MediaTypeExists(id))
                return false;
            var toRemove = await _context.MediaTypes.FindAsync(id);
            _context.MediaTypes.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}