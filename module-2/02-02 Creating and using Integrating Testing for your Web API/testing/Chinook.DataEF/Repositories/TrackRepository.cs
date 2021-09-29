using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ChinookContext _context;

        public TrackRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> TrackExists(int id) =>
            await _context.Tracks.AnyAsync(i => i.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<Track>> GetAll() =>
            await _context.Tracks.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<Track> GetById(int id) =>
            await _context.Tracks.FindAsync(id);

        public async Task<Track> Add(Track newTrack)
        {
            await _context.Tracks.AddAsync(newTrack);
            await _context.SaveChangesAsync();
            return newTrack;
        }

        public async Task<bool> Update(Track track)
        {
            if (!await TrackExists(track.Id))
                return false;
            _context.Tracks.Update(track);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await TrackExists(id))
                return false;
            var toRemove = await _context.Tracks.FindAsync(id);
            _context.Tracks.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Track>> GetByAlbumId(int id) =>
            await _context.Tracks.Where(a => a.AlbumId == id).AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<Track>> GetByGenreId(int id) =>
            await _context.Tracks.Where(a => a.GenreId == id).AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<Track>> GetByMediaTypeId(int id) =>
            await _context.Tracks.Where(a => a.MediaTypeId == id).AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<Track>> GetByPlaylistId(int id) =>
            await _context.PlaylistTracks.Where(p => p.PlaylistId == id).Select(p => p.Track).AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<Track>> GetByArtistId(int id) =>
            await _context.Albums.Where(a => a.ArtistId == 5).SelectMany(t => t.Tracks).AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<List<Track>> GetByInvoiceId(int id) => await _context.Tracks
            .Where(c => c.InvoiceLines.Any(o => o.InvoiceId == id))
            .AsNoTrackingWithIdentityResolution().ToListAsync();
    }
}