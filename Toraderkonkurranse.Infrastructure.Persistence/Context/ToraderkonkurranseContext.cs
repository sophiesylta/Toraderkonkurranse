using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toraderkonkurranse.Domene;

namespace Toraderkonkurranse.Infrastructure.Persistence.Context
{
    public class ToraderkonkurranseContext : DbContext
    {
        public ToraderkonkurranseContext(DbContextOptions<ToraderkonkurranseContext> options) : base(options) { }

        public DbSet<Person> Personer { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<Konkurranse> Konkurranser { get; set; }
        public DbSet<KonkurranseDommer> KonkurranseDommer { get; set; }
        public DbSet<Deltaker> Deltakere { get; set; }
        public DbSet<Arrangement> Arrangement { get; set; }
        public DbSet<Deltakelse> Deltakelse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Når Person slettes vil referansen i konkurranseDommer også slettes
            //modelBuilder.Entity<KonkurranseDommer>().HasOne(e => e.person).WithMany().OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Deltakelse>().HasOne(e => e.person).WithMany().OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Person>().Ignore(x => x.Fødselsnummer);
            //modelBuilder.Entity<Person>().HasIndex(u => u._Fødselsnummer).IsUnique();
            //modelBuilder.Entity<Person>().HasIndex(e => e.Kallenavn).IsUnique();
        }
    }
}
