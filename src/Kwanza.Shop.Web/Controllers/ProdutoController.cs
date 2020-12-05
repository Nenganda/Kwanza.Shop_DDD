using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Kwanza.Shop.Web.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly InterfaceProdutoApp _interfaceProdutoApp;

        //Pegar Usuário Logado
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InterfaceCompraUsuarioApp _interfaceCompraUsuarioApp;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProdutoController(InterfaceProdutoApp interfaceProdutoApp, 
            UserManager<ApplicationUser> userManager,
            InterfaceCompraUsuarioApp interfaceCompraUsuarioApp,
            IWebHostEnvironment webHostEnvironment)
        {
            _interfaceProdutoApp = interfaceProdutoApp;
            _userManager = userManager;
            _interfaceCompraUsuarioApp = interfaceCompraUsuarioApp;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProdutoController
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _interfaceProdutoApp.ListarProdutosUsuario(idUsuario));
        }

        // GET: ProdutoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // GET: ProdutoController/Create
        public  async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {

                //Setar o Usuário Logado ao objecto
                var idUsuario = await RetornarIdUsuarioLogado();
                produto.UserId = idUsuario;

                await _interfaceProdutoApp.AddProduct(produto);
                if (produto.Notificacoes.Any())
                {
                    foreach (var item in produto.Notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.Mensagem);
                    }

                    return View("Create", produto);
                }
                
                await SalvarImagemProduto(produto);
            }
            catch
            {
                return View("Create", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _interfaceProdutoApp.UpdateProduct(produto);
                if (produto.Notificacoes.Any())
                {
                    foreach (var item in produto.Notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.Mensagem);
                    }

                    //Mostrar alerta quando ocorrer um Erro
                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique, ocorreu algum erro!";

                    return View("Edit", produto);
                }
            }
            catch
            {
                return View("Edit", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // POST: ProdutoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _interfaceProdutoApp.GetEntityById(id);

                await _interfaceProdutoApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Metodo q pegar Usuário Logado
        private async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);

            return idUsuario.Id;
        }

        //Listar Todos os produtos por vender na Pagina
        [AllowAnonymous] //Permitir acesso sem Autenticação do Usuário
        [HttpGet("/api/ListarProdutosComEstoque")]
        public async Task<JsonResult> ListarProdutosComEstoque()
        {
            return Json(await _interfaceProdutoApp.ListarProdutosComEstoque());
        }

        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarIdUsuarioLogado();
            return View(await _interfaceProdutoApp.ListarProdutosCarrinhoUsuario(idUsuario));
        }

        // GET: ProdutoController/Delete/5
        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _interfaceProdutoApp.ObterProdutoCarrinho(id));
        }

        // POST: ProdutoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _interfaceCompraUsuarioApp.GetEntityById(id);

                await _interfaceCompraUsuarioApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch
            {
                return View();
            }
        }

        #region
        public async Task SalvarImagemProduto(Produto produtoTela)
        {
            try
            {
                var produto = await _interfaceProdutoApp.GetEntityById(produtoTela.Id);

                if (produtoTela.Imagem != null)
                {
                    var webRoot = _webHostEnvironment.WebRootPath;
                    var permissionSet = new PermissionSet(PermissionState.Unrestricted);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Append, string.Concat(webRoot, "/img/produtos"));
                    permissionSet.AddPermission(writePermission);

                    var Extension = System.IO.Path.GetExtension(produtoTela.Imagem.FileName);

                    var NomeArquivo = string.Concat(produto.Id.ToString(), Extension);

                    var diretorioArquivoSalvar = string.Concat(webRoot, "\\img\\produtos\\", NomeArquivo);

                    produtoTela.Imagem.CopyTo(new FileStream(diretorioArquivoSalvar, FileMode.Create));

                    produto.Url = string.Concat("https://localhost:44316", "/img/produtos/", NomeArquivo);

                    await _interfaceProdutoApp.UpdateProduct(produto);
                }

            }
            catch (Exception erro)
            {
            }
        }
        #endregion
    }
}
