using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryProduto : RepositoryGenerics<Produto>, IProduto
    {
        private readonly DbContextOptions<ApplicationContext> _optionsbuilder;

        public RepositoryProduto()
        {
            _optionsbuilder = new DbContextOptions<ApplicationContext>();
        }

        public async Task<List<Produto>> ListarProdutosUSuario(string userId)
        {
            using (var banco = new ApplicationContext(_optionsbuilder))
            {
                return await banco.Produtos.Where(p => p.UserId == userId)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}
