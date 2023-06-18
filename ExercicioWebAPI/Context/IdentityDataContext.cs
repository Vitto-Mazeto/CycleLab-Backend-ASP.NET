using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExercicioWebAPI.Context
{
    public class IdentityDataContext : IdentityDbContext
    {
        public IdentityDataContext() { }

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }
    }
}
