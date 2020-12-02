using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryProduto : RepositoryGenerics<Produto>, IProduto
    {
        private readonly DbContextOptions<ApplicationContext> _optionsbuilder;

        public RepositoryProduto()
        {
            _optionsbuilder = new DbContextOptions<ApplicationContext>();
        }

        //Listar Todos os produtos por vender na Pagina
        public async Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto)
        {
            using (var banco = new ApplicationContext(_optionsbuilder))
            {
                return await banco.Produtos.Where(exProduto).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userId)
        {

            using (var banco = new ApplicationContext(_optionsbuilder))
            {
                var produtosCarrinhoUsuario = await (from p in banco.Produtos
                                                     join c in banco.ComprasUsuarios on p.Id equals c.IdProduto
                                                     where c.UserId.Equals(userId) && c.Situacao == EstadoCompra.Carrinho
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra = c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id
                                                     }
                                                    ).AsNoTracking().ToListAsync();

                return produtosCarrinhoUsuario;
            }
        }

        public async Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho)
        {
            using (var banco = new ApplicationContext(_optionsbuilder))
            {
                var produtosCarrinhoUsuario = await (from p in banco.Produtos
                                                     join c in banco.ComprasUsuarios on p.Id equals c.IdProduto
                                                     where c.Id.Equals(idProdutoCarrinho) && c.Situacao == EstadoCompra.Carrinho
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra = c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id
                                                     }
                                                    ).AsNoTracking().FirstOrDefaultAsync();

                return produtosCarrinhoUsuario;
            }
        }

        public async Task<List<Produto>> ListarProdutosUSuario(string userId)
        {
            using (var banco = new ApplicationContext(_optionsbuilder))
            {
                return await banco.Produtos.Where(p => p.UserId == userId)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}
