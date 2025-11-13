using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace FilminurkTARpe24_Markus.ServiceInterface
{
    public interface IMovieServices //see on interface. asub .core/serviceinterface
    {
        Task<Movie> Create(MoviesDTO dto);
        Task<Movie> Delete(Guid id);
        Task<Movie> DetailsAsync(Guid id);
        Task<Movie> Update(MoviesDTO dto);
    }
}
