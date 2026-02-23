using Filminurk.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Actors
{
    public class ActorsIndexViewModel
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
    }
    public enum ActorRating
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }
}