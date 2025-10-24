using Filminurk.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Filmnurk.Data
{
    public class FilminurkTARpe24Context : DbContext
    {
        public FilminurkTARpe24Context(DbContextOptions<FilminurkTARpe24Context> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<FileToApi> FilesToApi { get; set; }
     }
}
