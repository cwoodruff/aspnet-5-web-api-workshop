using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Entities;
using ChinookASPNETWebAPI.Domain.Extensions;
using FluentValidation;

namespace ChinookASPNETWebAPI.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public async Task<IEnumerable<TrackApiModel>> GetAllTrack()
        {
            List<Track> tracks = await _trackRepository.GetAll();
            var trackApiModels = tracks.ConvertAll();

            return trackApiModels;
        }

        public async Task<TrackApiModel> GetTrackById(int id)
        {
            var track = await _trackRepository.GetById(id);
            if (track == null) return null;
            var trackApiModel = track.Convert();
            trackApiModel.Genre = await GetGenreById(trackApiModel.GenreId);
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

            return trackApiModel;
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

            var track = newTrackApiModel.Convert();

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