using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
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