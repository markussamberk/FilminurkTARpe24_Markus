namespace FilminurkTARpe24_Markus.Services
{
    public class MovieServices
    {
    }

    public async Task<Movie> DetailsAsync(Guid id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}

