using Microsoft.EntityFrameworkCore;

namespace Visma.Bootcamp.eShop.ApplicationCore.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }
    }
}
