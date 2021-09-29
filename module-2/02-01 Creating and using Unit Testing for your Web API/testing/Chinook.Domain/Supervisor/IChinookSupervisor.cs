using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;

namespace Chinook.Domain.Supervisor
{
    public interface IChinookSupervisor
    {
        Task<IEnumerable<AlbumApiModel>> GetAllAlbum();
        Task<AlbumApiModel?> GetAlbumById(int? id);
        Task<IEnumerable<AlbumApiModel>> GetAlbumByArtistId(int id);

        Task<AlbumApiModel> AddAlbum(AlbumApiModel newAlbumApiModel);

        Task<bool> UpdateAlbum(AlbumApiModel albumApiModel);
        Task<bool> DeleteAlbum(int id);
        Task<IEnumerable<ArtistApiModel>> GetAllArtist();
        Task<ArtistApiModel> GetArtistById(int id);

        Task<ArtistApiModel> AddArtist(ArtistApiModel newArtistApiModel);

        Task<bool> UpdateArtist(ArtistApiModel artistApiModel);

        Task<bool> DeleteArtist(int id);
        Task<IEnumerable<CustomerApiModel>> GetAllCustomer();
        Task<CustomerApiModel> GetCustomerById(int id);

        Task<IEnumerable<CustomerApiModel>> GetCustomerBySupportRepId(int id);

        Task<CustomerApiModel> AddCustomer(CustomerApiModel newCustomerApiModel);

        Task<bool> UpdateCustomer(CustomerApiModel customerApiModel);

        Task<bool> DeleteCustomer(int id);
        Task<IEnumerable<EmployeeApiModel>> GetAllEmployee();
        Task<EmployeeApiModel> GetEmployeeById(int id);
        Task<EmployeeApiModel> GetEmployeeReportsTo(int id);

        Task<EmployeeApiModel> AddEmployee(EmployeeApiModel newEmployeeApiModel);

        Task<bool> UpdateEmployee(EmployeeApiModel employeeApiModel);

        Task<bool> DeleteEmployee(int id);

        Task<IEnumerable<EmployeeApiModel>> GetEmployeeDirectReports(int id);

        Task<IEnumerable<EmployeeApiModel>> GetDirectReports(int id);
        Task<IEnumerable<GenreApiModel>> GetAllGenre();
        Task<GenreApiModel> GetGenreById(int id);

        Task<GenreApiModel> AddGenre(GenreApiModel newGenreApiModel);

        Task<bool> UpdateGenre(GenreApiModel genreApiModel);
        Task<bool> DeleteGenre(int id);
        Task<IEnumerable<InvoiceLineApiModel>> GetAllInvoiceLine();
        Task<InvoiceLineApiModel> GetInvoiceLineById(int id);

        Task<IEnumerable<InvoiceLineApiModel>> GetInvoiceLineByInvoiceId(int id);

        Task<IEnumerable<InvoiceLineApiModel>> GetInvoiceLineByTrackId(int id);

        Task<InvoiceLineApiModel> AddInvoiceLine(InvoiceLineApiModel newInvoiceLineApiModel);

        Task<bool> UpdateInvoiceLine(InvoiceLineApiModel invoiceLineApiModel);

        Task<bool> DeleteInvoiceLine(int id);
        Task<IEnumerable<InvoiceApiModel>> GetAllInvoice();
        Task<InvoiceApiModel> GetInvoiceById(int id);

        Task<IEnumerable<InvoiceApiModel>> GetInvoiceByCustomerId(int id);

        Task<InvoiceApiModel> AddInvoice(InvoiceApiModel newInvoiceApiModel);

        Task<bool> UpdateInvoice(InvoiceApiModel invoiceApiModel);

        Task<bool> DeleteInvoice(int id);

        Task<IEnumerable<InvoiceApiModel>> GetInvoiceByEmployeeId(int id);

        Task<IEnumerable<MediaTypeApiModel>> GetAllMediaType();
        Task<MediaTypeApiModel> GetMediaTypeById(int id);

        Task<MediaTypeApiModel> AddMediaType(MediaTypeApiModel newMediaTypeApiModel);

        Task<bool> UpdateMediaType(MediaTypeApiModel mediaTypeApiModel);

        Task<bool> DeleteMediaType(int id);
        Task<IEnumerable<PlaylistApiModel>> GetAllPlaylist();
        Task<PlaylistApiModel> GetPlaylistById(int id);

        Task<PlaylistApiModel> AddPlaylist(PlaylistApiModel newPlaylistApiModel);

        Task<bool> UpdatePlaylist(PlaylistApiModel playlistApiModel);

        Task<bool> DeletePlaylist(int id);

        Task<IEnumerable<PlaylistApiModel>> GetPlaylistByTrackId(int id);

        Task<IEnumerable<TrackApiModel>> GetAllTrack();
        Task<TrackApiModel> GetTrackById(int id);
        Task<IEnumerable<TrackApiModel>> GetTrackByAlbumId(int id);
        Task<IEnumerable<TrackApiModel>> GetTrackByGenreId(int id);

        Task<IEnumerable<TrackApiModel>> GetTrackByMediaTypeId(int id);

        Task<IEnumerable<TrackApiModel>> GetTrackByPlaylistId(int id);

        Task<TrackApiModel> AddTrack(TrackApiModel newTrackApiModel);

        Task<bool> UpdateTrack(TrackApiModel trackApiModel);
        Task<bool> DeleteTrack(int id);

        Task<IEnumerable<TrackApiModel>> GetTrackByArtistId(int id);
        Task<IEnumerable<TrackApiModel>> GetTrackByInvoiceId(int id);
    }
}