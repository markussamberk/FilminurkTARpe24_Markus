using Filminurk.Models.Actors;

namespace FilminurkTARpe24_Markus.Models.Actors
{
    public class ActorsDetailsViewModel
    {
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public List<string>? MoviesActedFor { get; set; }
        public Guid PortraitID { get; set; }

        // 3 õpilase andmetüüpi
        public ActorRating? ActorRating { get; set; }
        public int? Salary { get; set; }
        public int? Height { get; set; }

        // andmebaasi jaoks
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
