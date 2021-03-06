﻿using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    //É usado esta classe quando aquisição do select terá Regras
    public interface IServiceProduto
    {
        Task AddProduct(Produto produto);
        Task UpdateProduct(Produto produto);

        //Listar Todos os produtos por vender na Pagina
        Task<List<Produto>> ListarProdutosComEstoque();
    }
}
