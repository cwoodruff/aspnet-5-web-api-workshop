using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.Repositories
{
    public interface IMediaTypeRepository : IDisposable
    {
        Task<List<MediaType>> GetAll();
        Task<MediaType> GetById(int id);
        Task<MediaType> Add(MediaType newMediaType);
        Task<bool> Update(MediaType mediaType);
        Task<bool> Delete(int id);
    }
}