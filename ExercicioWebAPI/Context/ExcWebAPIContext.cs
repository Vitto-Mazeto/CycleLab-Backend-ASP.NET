using ExercicioWebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExercicioWebAPI.Context
{
    public class ExcWebAPIContext : DbContext
    {
        public ExcWebAPIContext(DbContextOptions<ExcWebAPIContext> options) : base(options) { }

        public DbSet<Amostra> Amostras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
