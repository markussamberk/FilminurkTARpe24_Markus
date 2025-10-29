using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using FilminurkTARpe24_Markus.ServiceInterface;
using Filmnurk.Data;

namespace Filminurk.ApplicationServices.Services
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

        public async Task<Movie> Create(MoviesDTO dto)
        {
            Movie movie = new Movie();
            movie.ID = dto.ID;
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = dto.CurrentRating;
            movie.TimesWatched = dto.TimesWatched;
            movie.Length = dto.Length;
            movie.Budget = dto.Budget;
            movie.FirstPublished = dto.FirstPublished;
            movie.Actors = dto.Actors;
            movie.Director = dto.Director;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public Task<Movie> DetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
