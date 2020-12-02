using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryCompraUsuario : RepositoryGenerics<CompraUsuario>, ICompraUsuario
    {
        private readonly DbContextOptions<ApplicationContext> _optionsbuilder;

        public RepositoryCompraUsuario()
        {
            _optionsbuilder = new DbContextOptions<ApplicationContext>();
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using (var banco = new ApplicationContext(_optionsbuilder)) 
            {
                return await banco.ComprasUsuarios.CountAsync(c => c.UserId.Equals(userId) && c.Situacao == EstadoCompra.Carrinho);
            }
        }
    }
}
