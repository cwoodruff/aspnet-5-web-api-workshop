using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.Entities;

namespace Chinook.Domain.Repositories
{
    public interface IAlbumRepository : IDisposable
    {
        Task<List<Album>> GetAll();
        Task<Album> GetById(int? id);
        Task<List<Album>> GetByArtistId(int id);
        Task<Album> Add(Album newAlbum);
        Task<bool> Update(Album album);
        Task<bool> Delete(int id);
    }
}