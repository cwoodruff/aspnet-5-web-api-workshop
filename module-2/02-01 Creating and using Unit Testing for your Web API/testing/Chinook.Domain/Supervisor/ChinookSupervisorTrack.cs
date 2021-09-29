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
        public async Task<IEnumerable<TrackApiModel>> GetAllTrack()
        {
            List<Track> tracks = await _trackRepository.GetAll();
            var trackApiModels = tracks.ConvertAll();
            foreach (var track in trackApiModels)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Track-", track.Id), track, cacheEntryOptions);
            }

            return trackApiModels;
        }

        public async Task<TrackApiModel> GetTrackById(int id)
        {
            var trackApiModelCached = _cache.Get<TrackApiModel>(string.Concat("Track-", id));

            if (trackApiModelCached != null)
            {
                return trackApiModelCached;
            }
            else
            {
                var trackApiModel = await (await _trackRepository.GetById(id)).ConvertAsync();
                trackApiModel.Genre = await GetGenreById(trackApiModel.GenreId.GetValueOrDefault());
                trackApiModel.Album = await GetAlbumById(trackApiModel.AlbumId);
                trackApiModel.MediaType = await GetMediaTypeById(trackApiModel.MediaTypeId);
                if (trackApiModel.Album != null)
                {
                    trackApiModel.AlbumName = trackApiModel.Album.Title;
                }

                trackApiModel.MediaTypeName = trackApiModel.MediaType.Name;
                if (trackApiModel.Genre != null)
                {
                    trackApiModel.GenreName = trackApiModel.Genre.Name;
                }

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?)"Track-", trackApiModel.Id), trackApiModel, cacheEntryOptions);

                return trackApiModel;
            }
        }

        public async Task<IEnumerable<TrackApiModel>> GetTrackByAlbumId(int id)
        {
            var tracks = await _trackRepository.GetByAlbumId(id);
            return tracks.ConvertAll();
        }

        public async Task<IEnumerable<TrackApiModel>> GetTrackByGenreId(int id)
        {
            var tracks = await _trackRepository.GetByGenreId(id);
            return tracks.ConvertAll();
        }

        public async Task<IEnumerable<TrackApiModel>> GetTrackByMediaTypeId(int id)
        {
            var tracks = await _trackRepository.GetByMediaTypeId(id);
            return tracks.ConvertAll();
        }

        public async Task<IEnumerable<TrackApiModel>> GetTrackByPlaylistId(int id)
        {
            var tracks = await _trackRepository.GetByPlaylistId(id);
            return tracks.ConvertAll();
        }

        public async Task<TrackApiModel> AddTrack(TrackApiModel newTrackApiModel)
        {
            await _trackValidator.ValidateAndThrowAsync(newTrackApiModel);
            var track = await newTrackApiModel.ConvertAsync();

            await _trackRepository.Add(track);
            newTrackApiModel.Id = track.Id;
            return newTrackApiModel;
        }

        public async Task<bool> UpdateTrack(TrackApiModel trackApiModel)
        {
            await _trackValidator.ValidateAndThrowAsync(trackApiModel);
            var track = await _trackRepository.GetById(trackApiModel.Id);

            if (track == null) return false;
            track.Id = trackApiModel.Id;
            track.Name = trackApiModel.Name;
            track.AlbumId = trackApiModel.AlbumId;
            track.MediaTypeId = trackApiModel.MediaTypeId;
            track.GenreId = trackApiModel.GenreId;
            track.Composer = trackApiModel.Composer ?? string.Empty;
            track.Milliseconds = trackApiModel.Milliseconds;
            track.Bytes = trackApiModel.Bytes;
            track.UnitPrice = trackApiModel.UnitPrice;

            return await _trackRepository.Update(track);
        }

        public Task<bool> DeleteTrack(int id)
            => _trackRepository.Delete(id);

        public async Task<IEnumerable<TrackApiModel>> GetTrackByArtistId(int id)
        {
            var tracks = await _trackRepository.GetByArtistId(id);
            return tracks.ConvertAll();
        }

        public async Task<IEnumerable<TrackApiModel>> GetTrackByInvoiceId(int id)
        {
            var tracks = await _trackRepository.GetByInvoiceId(id);
            return tracks.ConvertAll();
        }
    }
}