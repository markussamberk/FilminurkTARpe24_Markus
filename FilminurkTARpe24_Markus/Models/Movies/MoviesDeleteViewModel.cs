namespace FilminurkTARpe24_Markus.Models.Movies
{
    public class MoviesDeleteViewModel
    {
        public Guid? ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public string? Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }

        
        public string? CountryOfOrigin { get; set; }
        public Genre? MovieGenre { get; set; }
        public 
    }
}
