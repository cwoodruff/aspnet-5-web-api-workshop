using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface ITrackRepository : IDisposable
    {
        Task<List<Track>> GetAll();
        Task<Track> GetById(int id);
        Task<List<Track>> GetByAlbumId(int id);
        Task<List<Track>> GetByGenreId(int id);
        Task<List<Track>> GetByMediaTypeId(int id);
        Task<Track> Add(Track newTrack);
        Task<bool> Update(Track track);
        Task<bool> Delete(int id);
        Task<List<Track>> GetByInvoiceId(int id);
        Task<List<Track>> GetByPlaylistId(int id);
        Task<List<Track>> GetByArtistId(int id);
    }
}