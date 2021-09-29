using Chinook.Domain.ApiModels;
using Chinook.Domain.Repositories;
using Chinook.Domain.Validation;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor : IChinookSupervisor
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IInvoiceLineRepository _invoiceLineRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IMemoryCache _cache;

        private readonly IValidator<AlbumApiModel> _albumValidator;
        private readonly IValidator<ArtistApiModel> _artistValidator;
        private readonly IValidator<CustomerApiModel> _customerValidator;
        private readonly IValidator<EmployeeApiModel> _employeeValidator;
        private readonly IValidator<GenreApiModel> _genreValidator;
        private readonly IValidator<InvoiceApiModel> _invoiceValidator;
        private readonly IValidator<InvoiceLineApiModel> _invoiceLineValidator;
        private readonly IValidator<MediaTypeApiModel> _mediaTypeValidator;
        private readonly IValidator<PlaylistApiModel> _playlistValidator;
        private readonly IValidator<TrackApiModel> _trackValidator;

        public ChinookSupervisor(IAlbumRepository albumRepository,
            IArtistRepository artistRepository,
            ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository,
            IGenreRepository genreRepository,
            IInvoiceLineRepository invoiceLineRepository,
            IInvoiceRepository invoiceRepository,
            IMediaTypeRepository mediaTypeRepository,
            IPlaylistRepository playlistRepository,
            ITrackRepository trackRepository,
            IMemoryCache memoryCache,
            IValidator<AlbumApiModel> albumValidator,
            IValidator<ArtistApiModel> artistValidator,
            IValidator<CustomerApiModel> customerValidator,
            IValidator<EmployeeApiModel> employeeValidator,
            IValidator<GenreApiModel> genreValidator,
            IValidator<InvoiceApiModel> invoiceValidator,
            IValidator<InvoiceLineApiModel> invoiceLineValidator,
            IValidator<MediaTypeApiModel> mediaTypeValidator,
            IValidator<PlaylistApiModel> playlistValidator,
            IValidator<TrackApiModel> trackValidator
        )
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _genreRepository = genreRepository;
            _invoiceLineRepository = invoiceLineRepository;
            _invoiceRepository = invoiceRepository;
            _mediaTypeRepository = mediaTypeRepository;
            _playlistRepository = playlistRepository;
            _trackRepository = trackRepository;
            _cache = memoryCache;

            _albumValidator = albumValidator;
            _artistValidator = artistValidator;
            _customerValidator = customerValidator;
            _employeeValidator = employeeValidator;
            _genreValidator = genreValidator;
            _invoiceValidator = invoiceValidator;
            _invoiceLineValidator = invoiceLineValidator;
            _mediaTypeValidator = mediaTypeValidator;
            _playlistValidator = playlistValidator;
            _trackValidator = trackValidator;
        }
    }
}