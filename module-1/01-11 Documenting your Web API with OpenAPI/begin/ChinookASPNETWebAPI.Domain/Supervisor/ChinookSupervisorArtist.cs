using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Entities;
using ChinookASPNETWebAPI.Domain.Extensions;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace ChinookASPNETWebAPI.Domain.Supervisor
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
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("Artist-", artist.Id), artist, (TimeSpan)cacheEntryOptions);
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
                var artist = await _artistRepository.GetById(id);
                if (artist == null) return null;
                var artistApiModel = artist.Convert();
                artistApiModel.Albums = (await _albumRepository.GetByArtistId(artist.Id)).ConvertAll().ToList();

                var cacheEntryOptions = 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("Artist-", artistApiModel.Id), artistApiModel, (TimeSpan)cacheEntryOptions);

                return artistApiModel;
            }
        }

        public async Task<ArtistApiModel> AddArtist(ArtistApiModel newArtistApiModel)
        {
            await _artistValidator.ValidateAndThrowAsync(newArtistApiModel);

            var artist = newArtistApiModel.Convert();

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