using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public class ExcWebAPIContext : DbContext
    {
        public ExcWebAPIContext(DbContextOptions<ExcWebAPIContext> options) : base(options) { }

        public DbSet<Amostra> Amostras { get; set; }
        public DbSet<Exame> Exames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<Exame>()
                .HasOne(e => e.Amostra)
                .WithMany(a => a.Exames)
                .HasForeignKey(e => e.Id);
        }
    }
}
