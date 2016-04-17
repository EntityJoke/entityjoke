using EntityJoke.Core;
using EntityJoke.Process.Commands;
using EntityJokeTests.Core;
using NUnit.Framework;
using System;

namespace EntityJokeTests.Process
{
    [TestFixture]
    public class CommandUpdateGeneratorTest
    {

        CommandUpdateGenerator target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesAspects.GetInstance().Clear();
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(ProdutoTeste));
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(PrecoProduto));
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(ComparadorProdutos));
        }

        [Test]
        public void GeraUpdateCategoriaTeste()
        {
            CategoriaTeste categoria = new CategoriaTeste();
            categoria.Id = 2;
            categoria.Nome = "Comidas";

            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(categoria);

            categoria.Nome = "Comidas 1";

            target = new CommandUpdateGenerator(categoria);

            string insert = "UPDATE categoria_teste SET nome = 'Comidas 1' WHERE id = 2";

            Assert.That(target.GetCommand(), Is.EqualTo(insert));
        }

        [Test]
        public void GeraUpdateProdutoTeste()
        {
            ProdutoTeste produto = new ProdutoTeste();
            produto.Id = 3;

            produto.CategoriaTeste = new CategoriaTeste();
            produto.CategoriaTeste.Id = 4;
            produto.CategoriaTeste.Nome = "Congelados";

            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(produto);

            target = new CommandUpdateGenerator(produto);
            Assert.That(target.GetCommand(), Is.EqualTo(""));

            produto.CategoriaTeste = null;
            produto.Nome = "Lasanha";
            produto.Embalagem = "Caixa";
            produto.Marca = "Sadia";
            produto.Quantidade = "650";
            produto.UnidadeMedida = "g";

            target = new CommandUpdateGenerator(produto);

            string update = "";
            update += "UPDATE produto_teste ";
            update += "SET id_categoria_teste = null, ";
            update += "embalagem = 'Caixa', ";
            update += "marca = 'Sadia', ";
            update += "nome = 'Lasanha', ";
            update += "quantidade = '650', ";
            update += "unidade_medida = 'g' ";
            update += "WHERE id = 3";

            Assert.That(target.GetCommand(), Is.EqualTo(update));
        }

        [Test]
        public void GeraUpdatePrecoProduto()
        {
            DateTime dataIni = new DateTime(2015, 11, 07);
            DateTime dataFim = new DateTime(2015, 11, 09);

            PrecoProduto precoProduto = new PrecoProduto();
            precoProduto.Id = 10;
            precoProduto.Preco = 20;
            precoProduto.DataInicio = dataIni;
            precoProduto.DataFim = dataFim;

            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(precoProduto);

            target = new CommandUpdateGenerator(precoProduto);
            Assert.That(target.GetCommand(), Is.EqualTo(""));

            precoProduto.Produto = new Produto();
            precoProduto.Produto.Id = 4;
            precoProduto.Produto.Nome = "Trigo";

            target = new CommandUpdateGenerator(precoProduto);

            string update = "";
            update += "UPDATE preco_produto ";
            update += "SET id_produto = 4 ";
            update += "WHERE id = 10";

            Assert.That(target.GetCommand(), Is.EqualTo(update));
        }

        [Test]
        public void GeraUpdateComparadorProduto()
        {
            DateTime data = new DateTime(2015, 11, 07);

            ComparadorProdutos comparador = new ComparadorProdutos();
            comparador.Id = 20;

            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(comparador);

            target = new CommandUpdateGenerator(comparador);
            Assert.That(target.GetCommand(), Is.EqualTo(""));

            comparador.DataComparacao = data;

            comparador.ProdutoA = new Produto();
            comparador.ProdutoA.Id = 4;
            comparador.ProdutoA.Nome = "Trigo";

            comparador.ProdutoB = new Produto();
            comparador.ProdutoB.Id = 23;
            comparador.ProdutoB.Nome = "Macarrão";

            string update = "";
            update += "UPDATE comparador_produtos ";
            update += "SET data_comparacao = '" + data.GetDateTimeFormats()[54] + "', ";
            update += "id_produto_a = 4, ";
            update += "id_produto_b = 23 ";
            update += "WHERE id = 20";

            target = new CommandUpdateGenerator(comparador);
            Assert.That(target.GetCommand(), Is.EqualTo(update));
        }

    }
}
