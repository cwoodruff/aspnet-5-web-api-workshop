using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<ArtistApiModel>> GetAllArtist()
        {
            List<Artist> artists = await _artistRepository.GetAll();
            var artistApiModels = artists.ConvertAll();
            foreach (var artist in artistApiModels)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Artist-", artist.Id), artist, cacheEntryOptions);
            }

            return artistApiModels;
        }

        public async Task<ArtistApiModel> GetArtistById(int id)
        {
            var artistApiModelCached = _cache.Get<ArtistApiModel>(string.Concat("Artist-", id));

            if (artistApiModelCached != null)
            {
                return artistApiModelCached;
            }
            else
            {
                var artistApiModel = await (await _artistRepository.GetById(id)).ConvertAsync();
                artistApiModel.Albums = (await GetAlbumByArtistId(artistApiModel.Id)).ToList();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Artist-", artistApiModel.Id), artistApiModel, cacheEntryOptions);

                return artistApiModel;
            }
        }

        public async Task<ArtistApiModel> AddArtist(ArtistApiModel newArtistApiModel)
        {
            await _artistValidator.ValidateAndThrowAsync(newArtistApiModel);

            var artist = await newArtistApiModel.ConvertAsync();

            artist = await _artistRepository.Add(artist);
            newArtistApiModel.Id = artist.Id;
            return newArtistApiModel;
        }

        public async Task<bool> UpdateArtist(ArtistApiModel artistApiModel)
        {
            await _artistValidator.ValidateAndThrowAsync(artistApiModel);

            var artist = await _artistRepository.GetById(artistApiModel.Id);

            if (artist == null) return false;
            artist.Id = artistApiModel.Id;
            artist.Name = artistApiModel.Name ?? string.Empty;

            return await _artistRepository.Update(artist);
        }

        public Task<bool> DeleteArtist(int id)
            => _artistRepository.Delete(id);
    }
}