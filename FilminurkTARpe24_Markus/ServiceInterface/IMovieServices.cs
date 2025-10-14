namespace FilminurkTARpe24_Markus.ServiceInterface
{
    public interface IMovieServices //see on interface. asub .core/serviceinterface
    {
        Task<Movie> Create(MoviesDTO dto);
        Task<Movie> DetailsAsync(Guid id);
    }
}
