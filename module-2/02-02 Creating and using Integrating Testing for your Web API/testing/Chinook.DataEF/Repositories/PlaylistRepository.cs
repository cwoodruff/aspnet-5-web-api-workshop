using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCore.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ChinookContext _context;

        public PlaylistRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> PlaylistExists(int id) =>
            await _context.Playlists.AnyAsync(i => i.Id == id);

        public void Dispose() => _context.Dispose();

        public async Task<List<Playlist>> GetAll() =>
            await _context.Playlists.AsNoTrackingWithIdentityResolution().ToListAsync();

        public async Task<Playlist> GetById(int id) =>
            await _context.Playlists.FindAsync(id);

        public async Task<Playlist> Add(Playlist newPlaylist)
        {
            await _context.Playlists.AddAsync(newPlaylist);
            await _context.SaveChangesAsync();
            return newPlaylist;
        }

        public async Task<bool> Update(Playlist playlist)
        {
            if (!await PlaylistExists(playlist.Id))
                return false;
            _context.Playlists.Update(playlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await PlaylistExists(id))
                return false;
            var toRemove = await _context.Playlists.FindAsync(id);
            _context.Playlists.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Playlist>> GetByTrackId(int id)
        {
            return await _context.Playlists
                .Where(c => c.PlaylistTracks.Any(o => o.TrackId == id))
                .AsNoTrackingWithIdentityResolution().ToListAsync();
        }
    }
}