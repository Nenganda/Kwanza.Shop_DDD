

var ObjetoVenda = new Object();

ObjetoVenda.AdicionarCarrinho = function (idProduto) {

    var nome = $("#nome_" + idProduto).val();
    var qtd = $("#qtd_" + idProduto).val();

    $.ajax({
        type: 'POST',
        url: "/api/AdicionarProdutoCarrinho",
        dataType: "JSON",
        cache: false,
        async: true,
        data: {
            "id": idProduto, "nome": nome, "qtd": qtd
        },
        success: function (data) {

            if (data.sucesso) {
                // 1 alert-success // 2 alert-warning //3 alert-danger
                ObjetoAlerta.AlertarTela(1, "Produto adicionando no carrinho!");
            }
            else {
                // 1 alert-success // 2 alert-warning //3 alert-danger
                ObjetoAlerta.AlertarTela(2, "Necessário efectuar o login!");
            }
        }
    });
}

ObjetoVenda.CarregaProdutos = function () {
    $.ajax({
        type: 'GET',
        url: "/api/ListarProdutosComEstoque",
        dataType: "JSON",
        cache: false,
        async: true,
        success: function (data) {

            var htmlConteudo = "";
            data.forEach(function (Entitie) {
                htmlConteudo += "<div class='col-xl-3 col-lg-4 col-md-4 col-sm-4 d-flex align-items-lg-stretch'>";
                htmlConteudo += "<div class='card text-center bg-light mb-4'>";

                var idNome = "nome_" + Entitie.id;
                var idQtd = "qtd_" + Entitie.id;

                if (Entitie.url != null && Entitie.url != "" && Entitie.url != undefined)
                {
                    htmlConteudo += "<img class='card-img-top' width='200' height='200' src='" + Entitie.url + "'/></br>";
                }
                else
                {
                    htmlConteudo += "<img class='card-img-top' width='200' height='100' src='https://localhost:44316/img/produtos/Sem_Imagem.png'/></br>";
                }

                htmlConteudo += "<div class='card-header'> " + Entitie.nome + "</div>";

                htmlConteudo += "<div class='card-body'>";
                htmlConteudo += "<h5 class='card-title'>" + Entitie.valor + " - Kz</h5>";
                htmlConteudo += "<p class='card-text'>" + Entitie.descricao + "</p>";
                htmlConteudo += "</div>";

                if (Entitie.qtdEstoque > 0) {

                    htmlConteudo += "Quantidade : <input type'number' value='1' min='1' max='" + Entitie.qtdEstoque + "' id='" + idQtd + "'>";

                    htmlConteudo += "<input class='btn btn-success' type='button' onclick='ObjetoVenda.AdicionarCarrinho(" + Entitie.id + ")' value ='Comprar'> ";
                    htmlConteudo += "<small class='text-success'>Estoque: " + Entitie.qtdEstoque + "</small>";

                }
                else {

                    htmlConteudo += "<div><button class='btn btn - light disabled' disabled><small>Aguardando Reabastecimento</small></button></div>";
                }

                htmlConteudo += "</div>";
                htmlConteudo += "</div>";
            });

            $("#DivVenda").html(htmlConteudo);
        }
    })
}

ObjetoVenda.CarregaQtdCarrinho = function () {

    $("#qtdCarrinho").text("(0)");

    $.ajax({
        type: 'GET',
        url: "/api/QtdProdutosCarrinho",
        dataType: "JSON",
        cache: false,
        async: true,
        success: function (data) {

            if (data.sucesso) {
                $("#qtdCarrinho").text("(" + data.qtd + ")");
            }
        }
    });  

    //Cada 10s actualizar
    setTimeout(ObjetoVenda.CarregaQtdCarrinho, 10000);
}

$(function () {
    ObjetoVenda.CarregaProdutos();
    ObjetoVenda.CarregaQtdCarrinho();
});
