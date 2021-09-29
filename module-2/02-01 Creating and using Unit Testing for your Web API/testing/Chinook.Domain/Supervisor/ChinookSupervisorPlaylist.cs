using System;
using System.Collections.Generic;
using Chinook.Domain.ApiModels;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Entities;
using Chinook.Domain.Extensions;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public async Task<IEnumerable<PlaylistApiModel>> GetAllPlaylist()
        {
            List<Playlist> playlists = await _playlistRepository.GetAll();
            var playlistApiModels = playlists.ConvertAll();
            foreach (var playlist in playlistApiModels)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Playlist-", playlist.Id), playlist, cacheEntryOptions);
            }

            return playlistApiModels;
        }

        public async Task<PlaylistApiModel> GetPlaylistById(int id)
        {
            var playlistApiModelCached = _cache.Get<PlaylistApiModel>(string.Concat("Playlist-", id));

            if (playlistApiModelCached != null)
            {
                return playlistApiModelCached;
            }
            else
            {
                var playlistApiModel = await (await _playlistRepository.GetById(id)).ConvertAsync();
                playlistApiModel.Tracks = (await GetTrackByPlaylistId(playlistApiModel.Id)).ToList();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Playlist-", playlistApiModel.Id), playlistApiModel,
                    cacheEntryOptions);

                return playlistApiModel;
            }
        }

        public async Task<PlaylistApiModel> AddPlaylist(PlaylistApiModel newPlaylistApiModel)
        {
            await _playlistValidator.ValidateAndThrowAsync(newPlaylistApiModel);
            var playlist = await newPlaylistApiModel.ConvertAsync();

            playlist = await _playlistRepository.Add(playlist);
            newPlaylistApiModel.Id = playlist.Id;
            return newPlaylistApiModel;
        }

        public async Task<bool> UpdatePlaylist(PlaylistApiModel playlistApiModel)
        {
            await _playlistValidator.ValidateAndThrowAsync(playlistApiModel);
            var playlist = await _playlistRepository.GetById(playlistApiModel.Id);

            if (playlist == null) return false;
            playlist.Id = playlistApiModel.Id;
            playlist.Name = playlistApiModel.Name ?? string.Empty;

            return await _playlistRepository.Update(playlist);
        }

        public Task<bool> DeletePlaylist(int id)
            => _playlistRepository.Delete(id);

        public async Task<IEnumerable<PlaylistApiModel>> GetPlaylistByTrackId(int id)
        {
            var playlists = await _playlistRepository.GetByTrackId(id);
            return playlists.ConvertAll();
        }
    }
}