using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Entities;
using ChinookASPNETWebAPI.Domain.Extensions;
using FluentValidation;

namespace ChinookASPNETWebAPI.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public async Task<IEnumerable<AlbumApiModel>> GetAllAlbum()
        {
            List<Album> albums = await _albumRepository.GetAll();
            var albumApiModels = albums.ConvertAll();

            return albumApiModels;
        }

        public async Task<AlbumApiModel> GetAlbumById(int id)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null) return null;
            var albumApiModel = album.Convert();
            albumApiModel.ArtistName = (_artistRepository.GetById(album.ArtistId)).Result.Name;
            albumApiModel.Tracks = (await GetTrackByAlbumId(id)).ToList();

            return albumApiModel;
        }

        public async Task<IEnumerable<AlbumApiModel>> GetAlbumByArtistId(int id)
        {
            var albums = await _albumRepository.GetByArtistId(id);
            return albums.ConvertAll();
        }

        public async Task<AlbumApiModel> AddAlbum(AlbumApiModel newAlbumApiModel)
        {
            await _albumValidator.ValidateAndThrowAsync(newAlbumApiModel);

            var album = newAlbumApiModel.Convert();

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