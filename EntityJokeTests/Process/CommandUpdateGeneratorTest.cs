using EntityJoke.Core;
using EntityJokeTests.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Process
{
    [TestFixture]
    public class CommandUpdateGeneratorTest
    {

        CommandUpdateGenerator target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(ProdutoTeste));
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(PrecoProduto));
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(ComparadorProdutos));
        }

        [Test]
        public void GeraInsertCategoriaTeste()
        {
            CategoriaTeste categoria = new CategoriaTeste();
            categoria.Id = 2;
            categoria.Nome = "Comidas";

            target = new CommandUpdateGenerator(categoria);

            string insert = "UPDATE categoria_teste SET nome = 'Comidas' WHERE id = 2";

            Assert.That(target.GetCommand(), Is.EqualTo(insert));
        }

        [Test]
        public void GeraInsertProdutoTeste()
        {
            ProdutoTeste produto = new ProdutoTeste();
            produto.Id = 3;
            produto.Nome = "Lasanha";
            produto.Embalagem = "Caixa";
            produto.Marca = "Sadia";
            produto.Quantidade = "650";
            produto.UnidadeMedida = "g";

            produto.CategoriaTeste = new CategoriaTeste();
            produto.CategoriaTeste.Id = 4;
            produto.CategoriaTeste.Nome = "Congelados";

            target = new CommandUpdateGenerator(produto);

            string insert = "";
            insert += "UPDATE produto_teste ";
            insert += "SET id_categoria_teste = 4, ";
            insert += "embalagem = 'Caixa', ";
            insert += "marca = 'Sadia', ";
            insert += "nome = 'Lasanha', ";
            insert += "quantidade = '650', ";
            insert += "unidade_medida = 'g' ";
            insert += "WHERE id = 3";

            Assert.That(target.GetCommand(), Is.EqualTo(insert));
        }

        [Test]
        public void GeraInsertPrecoProduto()
        {
            DateTime dataIni = new DateTime(2015, 11, 07);
            DateTime dataFim = new DateTime(2015, 11, 09);

            PrecoProduto precoProduto = new PrecoProduto();
            precoProduto.Id = 10;
            precoProduto.Preco = 20;
            precoProduto.DataInicio = dataIni;
            precoProduto.DataFim = dataFim;

            precoProduto.Produto = new Produto();
            precoProduto.Produto.Id = 4;
            precoProduto.Produto.Nome = "Trigo";

            target = new CommandUpdateGenerator(precoProduto);

            string insert = "";
            insert += "UPDATE preco_produto ";
            insert += "SET data_fim = '" + dataFim + "', ";
            insert += "data_inicio = '" + dataIni + "', ";
            insert += "preco = 20, ";
            insert += "id_produto = 4 ";
            insert += "WHERE id = 10";

            Assert.That(target.GetCommand(), Is.EqualTo(insert));
        }

        [Test]
        public void GeraInsertComparadorProduto()
        {
            DateTime data = new DateTime(2015, 11, 07);

            ComparadorProdutos comparador = new ComparadorProdutos();
            comparador.Id = 20;
            comparador.DataComparacao = data;

            comparador.ProdutoA = new Produto();
            comparador.ProdutoA.Id = 4;
            comparador.ProdutoA.Nome = "Trigo";

            comparador.ProdutoB = new Produto();
            comparador.ProdutoB.Id = 23;
            comparador.ProdutoB.Nome = "Macarrão";

            target = new CommandUpdateGenerator(comparador);

            string insert = "";
            insert += "UPDATE comparador_produtos ";
            insert += "SET data_comparacao = '" + data + "', ";
            insert += "id_produto_a = 4, ";
            insert += "id_produto_b = 23 ";
            insert += "WHERE id = 20";

            Assert.That(target.GetCommand(), Is.EqualTo(insert));
        }

    }
}
