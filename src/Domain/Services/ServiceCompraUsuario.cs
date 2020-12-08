using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceCompraUsuario : IServiceCompraUsuario
    {
        private readonly ICompraUsuario _compraUsuario;

        public ServiceCompraUsuario(ICompraUsuario compraUsuario)
        {
            _compraUsuario = compraUsuario;
        }

        public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _compraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Carrinho);
        }

        public async Task<CompraUsuario> ProdutosCompras(string userId)
        {
            return await _compraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Comprado);
        }
    }
}
