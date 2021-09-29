﻿using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
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