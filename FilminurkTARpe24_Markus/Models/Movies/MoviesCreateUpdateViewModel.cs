using Filminurk.Core.Domain;

namespace FilminurkTARpe24_Markus.Models.Movies
{
    public class MoviesCreateViewModel
    {
        public Guid? ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public string? Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }


        public string? TimesWatched { get; set; }
        public MovieGenre? MovieGenre { get; set; }
        public MovieGenre? SubGenre { get; set; }

        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
