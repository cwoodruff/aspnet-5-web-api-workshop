﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.Entities;

namespace ChinookASPNETWebAPI.Domain.Repositories
{
    public interface IPlaylistTrackRepository : IDisposable
    {
        Task<List<PlaylistTrack>> GetAll();
        Task<List<PlaylistTrack>> GetByPlaylistId(int id);
        Task<List<PlaylistTrack>> GetByTrackId(int id);
        Task<PlaylistTrack> Add(PlaylistTrack newPlaylistTrack);
        Task<bool> Update(PlaylistTrack playlistTrack);
        Task<bool> Delete(int id);
    }
}