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
    public class QuerySimpleTest
    {
        const string SELECT_PRODUTO = "p.id p_id, p.codigo_de_barras p_codigo_de_barras, p.nome p_nome, p.nome2 p_nome2, p.quantidade p_quantidade";
        const string SELECT_PRECO = "p.id p_id, p.data_fim p_data_fim, p.data_inicio p_data_inicio, p.preco p_preco";
        const string SELECT_ALTERACAO = "a.id a_id, a.data_alteracao a_data_alteracao";

        QuerySimple<Produto> targetProduto;
        QuerySimple<PrecoProduto> targetPreco;
        QuerySimple<AlteracaoPrecoProduto> targetAlteracao;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(AlteracaoPrecoProduto));
        }

        [Test]
        public void AssertSQLSimples()
        {
            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p";

            targetProduto = new QuerySimple<Produto>();
            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComUmaClausulaWhere()
        {
            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p ";
            sql += "WHERE p.id > 3";

            targetProduto = new QuerySimple<Produto>()
                .Where("produto.Id > " + 3);

            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComDuasClausulaWhere()
        {
            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p ";
            sql += "WHERE p.id > 3 AND p.nome='arroz'";

            targetProduto = new QuerySimple<Produto>()
                .Where("produto.Id > " + 3 + " AND produto.Nome='arroz'");

            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComTresClausulaWhere()
        {
            string condicao = "produto.Id > 3";
            condicao += " AND produto.Nome= 'arroz'";
            condicao += " AND produto.CodigoDeBarras ='78965436321'";

            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p ";
            sql += "WHERE p.id > 3 AND p.nome= 'arroz' AND p.codigo_de_barras ='78965436321'";

            targetProduto = new QuerySimple<Produto>()
                .Where(condicao);

            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComOrWhere()
        {
            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p ";
            sql += "WHERE p.id > 3 OR p.nome = 'arroz'";

            targetProduto = new QuerySimple<Produto>()
                .Where("produto.Id > " + 3 + " OR produto.Nome = 'arroz'");

            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComOrEAndWhere()
        {
            string condicao = "produto.Id > 3";
            condicao += " AND (produto.Nome = 'arroz'";
            condicao += " OR produto.CodigoDeBarras = '78965436321')";

            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p ";
            sql += "WHERE p.id > 3 AND (p.nome = 'arroz' OR p.codigo_de_barras = '78965436321')";

            targetProduto = new QuerySimple<Produto>()
                .Where(condicao);

            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLSimplesComUmOrdenador()
        {
            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p ";
            sql += "ORDER BY p.id";

            targetProduto = new QuerySimple<Produto>()
                .OrderBy("produto.Id");

            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLSimplesComDoisOrdenadores()
        {
            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p ";
            sql += "ORDER BY p.id DESC, p.nome";

            targetProduto = new QuerySimple<Produto>()
                .OrderBy("produto.Id DESC, produto.Nome");

            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComUmaCondicaoWhereEComUmOrdenador()
        {
            string sql = "";
            sql += "SELECT " + SELECT_PRODUTO + " ";
            sql += "FROM produto p ";
            sql += "WHERE p.nome2 NOT IS NULL ";
            sql += "ORDER BY p.codigo_de_barras";

            targetProduto = new QuerySimple<Produto>()
                .Where("produto.Nome2 NOT IS NULL")
                .OrderBy("produto.CodigoDeBarras");

            Assert.That(targetProduto.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLSimplesPrecoProduto()
        {
            string sql = "";
            sql += "SELECT " + SELECT_PRECO + ", ";
            sql += SELECT_PRODUTO.Replace("p.", "pr.").Replace("p_", "pr_") + " ";
            sql += "FROM preco_produto p ";
            sql += "LEFT JOIN produto pr ON (pr.id = p.id_produto)";

            targetPreco = new QuerySimple<PrecoProduto>();
            Assert.That(targetPreco.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComCondicaoSimplesPrecoProduto()
        {
            targetPreco = new QuerySimple<PrecoProduto>()
                .Where("precoProduto.Preco = 3");

            string sql = "";
            sql += "SELECT " + SELECT_PRECO + ", ";
            sql += SELECT_PRODUTO.Replace("p.", "pr.").Replace("p_", "pr_") + " ";
            sql += "FROM preco_produto p ";
            sql += "LEFT JOIN produto pr ON (pr.id = p.id_produto) ";
            sql += "WHERE p.preco = 3";

            Assert.That(targetPreco.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComCondicaoCompostaPrecoProduto()
        {
            targetPreco = new QuerySimple<PrecoProduto>()
                .Where("precoProduto.Produto.Nome = 'Arroz'");

            string sql = "";
            sql += "SELECT " + SELECT_PRECO + ", ";
            sql += SELECT_PRODUTO.Replace("p.", "pr.").Replace("p_", "pr_") + " ";
            sql += "FROM preco_produto p ";
            sql += "LEFT JOIN produto pr ON (pr.id = p.id_produto) ";
            sql += "WHERE pr.nome = 'Arroz'";

            Assert.That(targetPreco.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLSimplesAlteracaoPrecoProduto()
        {
            targetAlteracao = new QuerySimple<AlteracaoPrecoProduto>();

            string sql = "";
            sql += "SELECT a.id a_id, a.data_alteracao a_data_alteracao, ";
            sql += "p.id p_id, p.data_fim p_data_fim, p.data_inicio p_data_inicio, p.preco p_preco, ";
            sql += "pr.id pr_id, pr.codigo_de_barras pr_codigo_de_barras, pr.nome pr_nome, pr.nome2 pr_nome2, pr.quantidade pr_quantidade ";
            sql += "FROM alteracao_preco_produto a ";
            sql += "LEFT JOIN preco_produto p ON (p.id = a.id_preco_prod) ";
            sql += "LEFT JOIN produto pr ON (pr.id = p.id_produto)";

            Assert.That(targetAlteracao.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComCondicoesCompostasEOrderByCompostoAlteracaoPrecoProduto()
        {
            targetAlteracao = new QuerySimple<AlteracaoPrecoProduto>()
                .Where("AlteracaoPrecoProduto.PrecoProd.produto.CodigoDeBarras = '058764' AND TRUNC(alteracaoPrecoProduto.DataAlteracao) > '11/07/2015' AND AlteracaoPrecoProduto.PrecoProd.Preco < 4")
                .OrderBy("AlteracaoPrecoProduto.PrecoProd.DataInicio, AlteracaoPrecoProduto.PrecoProd.produto.Id ASC, alteracaoPrecoProduto.DataAlteracao DESC");

            string sql = "";
            sql += "SELECT a.id a_id, a.data_alteracao a_data_alteracao, ";
            sql += "p.id p_id, p.data_fim p_data_fim, p.data_inicio p_data_inicio, p.preco p_preco, ";
            sql += "pr.id pr_id, pr.codigo_de_barras pr_codigo_de_barras, pr.nome pr_nome, pr.nome2 pr_nome2, pr.quantidade pr_quantidade ";
            sql += "FROM alteracao_preco_produto a ";
            sql += "LEFT JOIN preco_produto p ON (p.id = a.id_preco_prod) ";
            sql += "LEFT JOIN produto pr ON (pr.id = p.id_produto) ";
            sql += "WHERE pr.codigo_de_barras = '058764' ";
            sql += "AND TRUNC(a.data_alteracao) > '11/07/2015' ";
            sql += "AND p.preco < 4 ";
            sql += "ORDER BY p.data_inicio, ";
            sql += "pr.id ASC, ";
            sql += "a.data_alteracao DESC";

            Assert.That(targetAlteracao.ToString(), Is.EqualTo(sql));
        }
    }
}
