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
        public async Task<IEnumerable<MediaTypeApiModel>> GetAllMediaType()
        {
            List<MediaType> mediaTypes = await _mediaTypeRepository.GetAll();
            var mediaTypeApiModels = mediaTypes.ConvertAll();

            foreach (var mediaType in mediaTypeApiModels)
            {
                var cacheEntryOptions = 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("MediaType-", mediaType.Id), mediaType, (TimeSpan)cacheEntryOptions);
            }

            return mediaTypeApiModels;
        }

        public async Task<MediaTypeApiModel> GetMediaTypeById(int id)
        {
            var mediaTypeApiModelCached = _cache.Get<MediaTypeApiModel>(string.Concat("MediaType-", id));

            if (mediaTypeApiModelCached != null)
            {
                return mediaTypeApiModelCached;
            }
            else
            {
                var mediaType = await _mediaTypeRepository.GetById(id);
                if (mediaType == null) return null;
                var mediaTypeApiModel = mediaType.Convert();
                mediaTypeApiModel.Tracks = (await GetTrackByMediaTypeId(mediaTypeApiModel.Id)).ToList();

                var cacheEntryOptions = 
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                        .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);;
                _cache.Set(string.Concat("MediaType-", mediaTypeApiModel.Id), mediaTypeApiModel, (TimeSpan)cacheEntryOptions);

                return mediaTypeApiModel;
            }
        }

        public async Task<MediaTypeApiModel> AddMediaType(MediaTypeApiModel newMediaTypeApiModel)
        {
            await _mediaTypeValidator.ValidateAndThrowAsync(newMediaTypeApiModel);

            var mediaType = newMediaTypeApiModel.Convert();

            mediaType = await _mediaTypeRepository.Add(mediaType);
            newMediaTypeApiModel.Id = mediaType.Id;
            return newMediaTypeApiModel;
        }

        public async Task<bool> UpdateMediaType(MediaTypeApiModel mediaTypeApiModel)
        {
            await _mediaTypeValidator.ValidateAndThrowAsync(mediaTypeApiModel);

            var mediaType = await _mediaTypeRepository.GetById(mediaTypeApiModel.Id);

            if (mediaType == null) return false;
            mediaType.Id = mediaTypeApiModel.Id;
            mediaType.Name = mediaTypeApiModel.Name ?? string.Empty;

            return await _mediaTypeRepository.Update(mediaType);
        }

        public Task<bool> DeleteMediaType(int id)
            => _mediaTypeRepository.Delete(id);
    }
}