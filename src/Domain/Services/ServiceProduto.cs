using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Services
{
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

            if(validaNome && validaValor)
            {
                produto.Estado = true;
                await _produto.Add(produto);
            }
        }

        public async Task UpdateProduct(Produto produto)
        {
            var validaNome = produto.ValidarPropriedadeString(produto.Nome, "Nome");

            var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor, "Valor");

            if (validaNome && validaValor)
            {
                await _produto.Update(produto);
            }
        }
    }
}
