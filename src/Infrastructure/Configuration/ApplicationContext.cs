using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :  base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CompraUsuario> ComprasUsuarios { get; set; }
        public DbSet<IdentityUser> IdentityUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Informar a que o campo (Id) perteente a tabela AspNetUsers é a chave Primaria
            builder.Entity<IdentityUser>().ToTable("AspNetUsers").HasKey(us => us.Id); 

            base.OnModelCreating(builder);  
        }

        private string GetStringConectionConfig()
        {
            string strCon = "Data Source=(localdb)\\mssqllocaldb;Database=Kwanza.Shop;Trusted_Connection=True;MultipleActiveResultSets=true";
            return strCon;
        }
    }
}
