using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.Repositories
{
    public interface IArtistRepository : IDisposable
    {
        Task<List<Artist>> GetAll();
        Task<Artist> GetById(int id);
        Task<Artist> Add(Artist newArtist);
        Task<bool> Update(Artist artist);
        Task<bool> Delete(int id);
    }
}