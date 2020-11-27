using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        //Listar Todos os produtos por vender na Pagina
        public async Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto)
        {
            using (var banco = new ApplicationContext(_optionsbuilder))
            {
                return await banco.Produtos.Where(exProduto).AsNoTracking().ToListAsync();
            }
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
