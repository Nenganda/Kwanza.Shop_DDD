using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceProduto
    {
        Task AddProduct(Produto produto);
        Task UpdateProduct(Produto produto);
    }
}
