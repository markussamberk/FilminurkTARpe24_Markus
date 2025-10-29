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
            movie.FirstPublished =(DateOnly)dto.FirstPublished;
            movie.Actors = dto.Actors; 
            movie.Director = dto.Director;
            movie.Length = dto.Length;
            movie.Budget = dto.Budget;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie> DetailsAsync(Guid id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Movie> Delete (Guid id)
        {
            var result = await _context.Movies
                .FirstOrDefaultAsync(m => m.ID == id);

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

