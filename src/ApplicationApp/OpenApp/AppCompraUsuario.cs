using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppCompraUsuario : InterfaceCompraUsuarioApp
    {
        private readonly ICompraUsuario _compraUsuario;
        private readonly IServiceCompraUsuario _serviceCompraUsuario;

        public AppCompraUsuario(ICompraUsuario compraUsuario, IServiceCompraUsuario serviceCompraUsuario)
        {
            _compraUsuario = compraUsuario;
            _serviceCompraUsuario = serviceCompraUsuario;
        }
        public async Task Add(CompraUsuario Objeto)
        {
            await _compraUsuario.Add(Objeto);
        }

        public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _serviceCompraUsuario.CarrinhoCompras(userId);
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            return await _compraUsuario.ConfirmaCompraCarrinhoUsuario(userId);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId)
        {
            return await _serviceCompraUsuario.ProdutosCompras(userId);
        }

        public async Task Delete(CompraUsuario Objeto)
        {
            await _compraUsuario.Delete(Objeto);
        }

        public async Task<CompraUsuario> GetEntityById(int Id)
        {
            return await _compraUsuario.GetEntityById(Id);
        }

        public async Task<List<CompraUsuario>> List()
        {
            return await _compraUsuario.List();
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            return await _compraUsuario.QuantidadeProdutoCarrinhoUsuario(userId);
        }

        public async Task Update(CompraUsuario Objeto)
        {
            await _compraUsuario.Update(Objeto);
        }
    }
}
