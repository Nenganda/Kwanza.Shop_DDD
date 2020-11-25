using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceProdutoApp : InterfaceGenericApp<Produto>
    {
        Task AddProduct(Produto produto);
        Task UpdateProduct(Produto produto);
    }
}