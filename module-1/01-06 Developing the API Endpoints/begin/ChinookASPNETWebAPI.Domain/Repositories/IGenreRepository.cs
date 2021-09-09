using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.Repositories
{
    public interface IGenreRepository : IDisposable
    {
        Task<List<Genre>> GetAll();
        Task<Genre> GetById(int id);
        Task<Genre> Add(Genre newGenre);
        Task<bool> Update(Genre genre);
        Task<bool> Delete(int id);
    }
}