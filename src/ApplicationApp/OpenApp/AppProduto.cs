using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppProduto : InterfaceProdutoApp
    {
        IProduto _produto;
        IServiceProduto _serviceProduto;

        public AppProduto(IProduto produto, IServiceProduto serviceProduto)
        {
            _produto = produto;
            _serviceProduto = serviceProduto;
        }
        
        
        public async Task AddProduct(Produto produto)
        {
            await _serviceProduto.AddProduct(produto);
        }

        public async Task UpdateProduct(Produto produto)
        {
            await _serviceProduto.UpdateProduct(produto);
        }


        public async Task Add(Produto Objeto)
        {
            await _produto.Add(Objeto);
        }

        public async Task Delete(Produto Objeto)
        {
            await _produto.Add(Objeto); ;
        }

        public async Task<Produto> GetEntityById(int Id)
        {
            return await _produto.GetEntityById(Id);
        }

        public async Task<List<Produto>> List()
        {
            return await _produto.List();
        }

        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
            return await _produto.ListarProdutosUSuario(userId);
        }

        public async Task Update(Produto Objeto)
        {
            await _produto.Update(Objeto);
        }
    }
}
