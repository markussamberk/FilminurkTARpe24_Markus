using Filminurk.Core.Domain;

namespace FilminurkTARpe24_Markus.Models.Movies
{
    public class MoviesIndexViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateOnly FirstPublished { get; set; }
        public double? CurrentRating { get; set; }

        public int? TimesWatched { get; set; }
        public int? Length { get; set; }
    }
}
