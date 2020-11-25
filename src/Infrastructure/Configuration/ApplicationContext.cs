using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :  base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        private string GetStringConectionConfig()
        {
            string strCon = "Data Source=(localdb)\\mssqllocaldb;Database=Kwanza.Shop;Trusted_Connection=True;MultipleActiveResultSets=true";
            return strCon;
        }
    }
}
