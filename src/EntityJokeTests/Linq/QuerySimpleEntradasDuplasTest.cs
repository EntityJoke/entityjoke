using EntityJoke.Core;
using EntityJoke.Linq;
using EntityJokeTests.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Linq
{
    [TestFixture]
    public class QuerySimpleEntradasDuplas
    {

        QuerySimple<ProductsComparator> target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(ProductsComparator));
        }

        [Test]
        public void GeraSQLSimplesProduto()
        {
            string sql = "";
            sql += "SELECT p.id p_id, p.codigo_de_barras p_codigo_de_barras, p.nome p_nome, p.nome2 p_nome2, p.quantidade p_quantidade ";
            sql += "FROM produto p ";
            sql += "WHERE p.id = 12";

            QuerySimple<Produto>  targetProduto = new QuerySimple<Produto>().Where("produto.Id = 12");
            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        //[Test]
        public void GeraSQLSimplesComparadorProduto()
        {
            target = new QuerySimple<ProductsComparator>()
                .Where("comparadorProdutos.ProdutoA.Id = 100");

            string sql = "";
            sql += "SELECT c.id c_id, c.data_comparacao c_data_comparacao, ";
            sql += "p.id p_id, p.codigo_de_barras p_codigo_de_barras, p.nome p_nome, p.nome2 p_nome2, p.quantidade p_quantidade, ";
            sql += "pr.id pr_id, pr.codigo_de_barras pr_codigo_de_barras, pr.nome pr_nome, pr.nome2 pr_nome2, pr.quantidade pr_quantidade ";
            sql += "FROM comparador_produtos c ";
            sql += "LEFT JOIN produto p ON (p.id = c.id_produto_a) ";
            sql += "LEFT JOIN produto pr ON (pr.id = c.id_produto_b) ";
            sql += "WHERE p.id = 100";

            Assert.That(target.ToString(), Is.EqualTo(sql));
        }
    }

}