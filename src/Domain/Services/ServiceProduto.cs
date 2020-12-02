using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    //É usado esta classe quando aquisição do select terá Regras
    public class ServiceProduto : IServiceProduto
    {
        private readonly IProduto _produto;

        public ServiceProduto(IProduto produto)
        {
            _produto = produto;
        }

        public async Task AddProduct(Produto produto)
        {
            var validaNome = produto.ValidarPropriedadeString(produto.Nome, "Nome");

            var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor, "Valor");

            var validaQtdEstoque = produto.ValidarPropriedadeDecimal(produto.QtdEstoque, "QtdEstoque");

            if(validaNome && validaValor && validaQtdEstoque)
            {
                produto.DataCadastro = DateTime.Now;
                produto.DataAlteracao = DateTime.Now;
                produto.Estado = true;
                await _produto.Add(produto);
            }
        }

        //Listar Todos os produtos por vender na Pagina
        public async Task<List<Produto>> ListarProdutosComEstoque()
        {
            return await _produto.ListarProdutos(p => p.QtdEstoque > 0);
        }

        public async Task UpdateProduct(Produto produto)
        {
            var validaNome = produto.ValidarPropriedadeString(produto.Nome, "Nome");

            var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor, "Valor");

            var validaQtdEstoque = produto.ValidarPropriedadeDecimal(produto.QtdEstoque, "QtdEstoque");

            if (validaNome && validaValor && validaQtdEstoque)
            {
                produto.DataAlteracao = DateTime.Now;
                await _produto.Update(produto);
            }
        }
    }
}
