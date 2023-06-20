using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
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
