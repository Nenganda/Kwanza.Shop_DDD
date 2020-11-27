using Domain.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceProduct
{
    public interface IProduto : IGeneric<Produto>
    {
        Task<List<Produto>> ListarProdutosUSuario(string userId);

        //Listar Todos os produtos por vender na Pagina
        Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto);
    }
}
