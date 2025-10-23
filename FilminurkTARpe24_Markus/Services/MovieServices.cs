using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using FilminurkTARpe24_Markus.ServiceInterface;
using Filmnurk.Data;
using Microsoft.EntityFrameworkCore;

namespace FilminurkTARpe24_Markus.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public MovieServices
            (
            FilminurkTARpe24Context context,
            IFilesServices filesServices
            )
        {
            _context = context;
            _filesServices = filesServices;
        }
    }

        public async Task<Movie> Create(MoviesDTO dto)
        {
            Movie movie = new Movie();
            movie.ID = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = dto.CurrentRating;
            movie.TimesWatched = dto.TimesWatched;
            movie.FirstPublished = dto.FirstPublished;
            movie.Actors = dto.Actors; 
            movie.Director = dto.Director;
            movie.MovieGenre = dto.MovieGenre;
            movie.SubGenre = dto.SubGenre;
            movie.EntryCreatedAt = dto.EntryCreatedAt;
            movie.EntryModifiedAt = DateTime.Now;
        }

        public async Task<Movie> DetailsAsync(Guid id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Movie> Delete (Guid id)
        {
            var result = await _context.Movies
                .FirstOrDefaultAsync(m = m.ID == id);

            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new FileToApiDTO
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    FilePath = y.FilePath,
                }).ToArrayAsync();

            await _filesServices.RemoveImageFromApi(images);
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}

