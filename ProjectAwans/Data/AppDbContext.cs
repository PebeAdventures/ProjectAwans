using Microsoft.EntityFrameworkCore;
using ProjectAwans.Models;
using ProjectAwans.Models.Enums;
using ProjektAwans.Models;

namespace ProjektAwans.Data
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
      {
      }

      public DbSet<Card> Cards { get; set; }
      public DbSet<CardColor> CardColors { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Card>()
             .HasOne(c => c.CardColor)
             .WithMany(cc => cc.Cards)
             .HasForeignKey(c => c.CardColorId)
             .OnDelete(DeleteBehavior.Restrict);

         modelBuilder.Entity<CardColor>().HasData(
     new CardColor { Id = 1, Name = CardColorEnum.Red.ToString() },
     new CardColor { Id = 2, Name = CardColorEnum.Yellow.ToString() },
     new CardColor { Id = 3, Name = CardColorEnum.Green.ToString() },
     new CardColor { Id = 4, Name = CardColorEnum.Blue.ToString() },
     new CardColor { Id = 5, Name = CardColorEnum.Black.ToString() },
     new CardColor { Id = 6, Name = CardColorEnum.Purple.ToString() }
 );

      }
   }
}