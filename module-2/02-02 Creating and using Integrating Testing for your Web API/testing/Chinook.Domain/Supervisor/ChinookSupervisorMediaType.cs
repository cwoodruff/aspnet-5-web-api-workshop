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
        public async Task<IEnumerable<MediaTypeApiModel>> GetAllMediaType()
        {
            List<MediaType> mediaTypes = await _mediaTypeRepository.GetAll();
            var mediaTypeApiModels = mediaTypes.ConvertAll();
            foreach (var mediaType in mediaTypeApiModels)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"MediaType-", mediaType.Id), mediaType, cacheEntryOptions);
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
                var mediaTypeApiModel = await (await _mediaTypeRepository.GetById(id)).ConvertAsync();
                mediaTypeApiModel.Tracks = (await GetTrackByMediaTypeId(mediaTypeApiModel.Id)).ToList();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"MediaType-", mediaTypeApiModel.Id), mediaTypeApiModel,
                    cacheEntryOptions);

                return mediaTypeApiModel;
            }
        }

        public async Task<MediaTypeApiModel> AddMediaType(MediaTypeApiModel newMediaTypeApiModel)
        {
            await _mediaTypeValidator.ValidateAndThrowAsync(newMediaTypeApiModel);
            var mediaType = await newMediaTypeApiModel.ConvertAsync();

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