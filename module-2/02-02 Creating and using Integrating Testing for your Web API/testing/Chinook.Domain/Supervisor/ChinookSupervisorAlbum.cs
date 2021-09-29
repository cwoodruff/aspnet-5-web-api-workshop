using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;
using Chinook.Domain.Extensions;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public async Task<IEnumerable<AlbumApiModel>> GetAllAlbum()
        {
            List<Album> albums = await _albumRepository.GetAll();
            var albumApiModels = albums.ConvertAll();
            foreach (var album in albumApiModels)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Album-", album.Id), album, cacheEntryOptions);
            }

            return albumApiModels;
        }

        public async Task<AlbumApiModel?> GetAlbumById(int? id)
        {
            var albumApiModelCached = _cache.Get<AlbumApiModel>(string.Concat("Album-", id));

            if (albumApiModelCached != null)
            {
                return albumApiModelCached;
            }
            else
            {
                var album = await _albumRepository.GetById(id);
                if (album == null) return null;
                var albumApiModel = await album.ConvertAsync();
                albumApiModel.ArtistName = (_artistRepository.GetById(albumApiModel.ArtistId)).Result.Name;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Album-", albumApiModel.Id), albumApiModel, cacheEntryOptions);

                return albumApiModel;
            }
        }

        public async Task<IEnumerable<AlbumApiModel>> GetAlbumByArtistId(int id)
        {
            var albums = await _albumRepository.GetByArtistId(id);
            return albums.ConvertAll();
        }

        public async Task<AlbumApiModel> AddAlbum(AlbumApiModel newAlbumApiModel)
        {
            await _albumValidator.ValidateAndThrowAsync(newAlbumApiModel);

            var album = await newAlbumApiModel.ConvertAsync();

            album = await _albumRepository.Add(album);
            newAlbumApiModel.Id = album.Id;
            return newAlbumApiModel;
        }

        public async Task<bool> UpdateAlbum(AlbumApiModel albumApiModel)
        {
            await _albumValidator.ValidateAndThrowAsync(albumApiModel);

            var album = await _albumRepository.GetById(albumApiModel.Id);

            if (album is null) return false;
            album.Id = albumApiModel.Id;
            album.Title = albumApiModel.Title;
            album.ArtistId = albumApiModel.ArtistId;

            return await _albumRepository.Update(album);
        }

        public Task<bool> DeleteAlbum(int id)
            => _albumRepository.Delete(id);
    }
}