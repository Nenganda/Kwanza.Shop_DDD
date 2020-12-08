using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryCompraUsuario : RepositoryGenerics<CompraUsuario>, ICompraUsuario
    {
        private readonly DbContextOptions<ApplicationContext> _optionsbuilder;

        public RepositoryCompraUsuario()
        {
            _optionsbuilder = new DbContextOptions<ApplicationContext>();
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            try
            {
                using (var banco = new ApplicationContext(_optionsbuilder))
                {
                    var compraUsuario = new CompraUsuario();
                    compraUsuario.ListaProdutos = new List<Produto>();

                    var produtosCarrinhoUsuario = await (from p in banco.Produtos
                                                         join c in banco.ComprasUsuarios on p.Id equals c.IdProduto
                                                         where c.UserId.Equals(userId) && c.Situacao == EstadoCompra.Carrinho
                                                         select c).AsNoTracking().ToListAsync();

                    produtosCarrinhoUsuario.ForEach(p =>
                    {
                        p.Situacao = EstadoCompra.Comprado;
                    });

                    banco.UpdateRange(produtosCarrinhoUsuario);
                    await banco.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception erro)
            {
                return false;
            }
        }

        public async Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado)
        {
            using (var banco = new ApplicationContext(_optionsbuilder))
            {
                var compraUsuario = new CompraUsuario();
                compraUsuario.ListaProdutos = new List<Produto>();

                var produtosCarrinhoUsuario = await (from p in banco.Produtos
                                                     join c in banco.ComprasUsuarios on p.Id equals c.IdProduto
                                                     where c.UserId.Equals(userId) && c.Situacao == estado
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra = p.QtdCompra,
                                                         IdProdutoCarrinho = p.IdProdutoCarrinho,
                                                         Url = p.Url
                                                     }).AsNoTracking().ToListAsync();

                compraUsuario.ListaProdutos = produtosCarrinhoUsuario;
                compraUsuario.ApplicationUser = await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                compraUsuario.QuantidadeProdutos = produtosCarrinhoUsuario.Count();
                compraUsuario.EnderecoCompleto = string.Concat(compraUsuario.ApplicationUser.Endereco, " - ", compraUsuario.ApplicationUser.ComplementoEndereco, " - Cep: ", compraUsuario.ApplicationUser.CEP);
                compraUsuario.ValorTotal = produtosCarrinhoUsuario.Sum(v => v.Valor);
                compraUsuario.Situacao = estado;
                return compraUsuario;
            }
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using (var banco = new ApplicationContext(_optionsbuilder)) 
            {
                return await banco.ComprasUsuarios.CountAsync(c => c.UserId.Equals(userId) && c.Situacao == EstadoCompra.Carrinho);
            }
        }
    }
}
