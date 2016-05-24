using EntityJoke.Core;
using EntityJoke.Linq;
using EntityJokeTests.Core;
using EntityJokeTests.EntidadesTestes;
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
        const string SELECT_PRODUCT_FOR_TEST = "p.id p_id, p.maker p_maker, p.name p_name, p.packing p_packing, p.quantity p_quantity, p.unit p_unit, c.id c_id, c.name c_name";
        const string SELECT_PRICE_PRODUCT = "p.id p_id, p.end_date p_end_date, p.init_date p_init_date, p.price p_price";
        const string SELECT_ALTERACAO = "a.id a_id, a.data_alteracao a_data_alteracao";

        QuerySimple<Produto> targetProduto;
        QuerySimple<ProductPrice> targetPreco;
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
            sql += "SELECT " + SELECT_PRODUCT_FOR_TEST + " ";
            sql += "FROM product_for_test p ";
            sql += "LEFT JOIN category_for_test c ON (c.id = p.id_category)";

            var targetProduct = new QuerySimple<ProductForTest>();
            Assert.That(targetProduct.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComCondicaoSimplesPrecoProduto()
        {
            targetPreco = new QuerySimple<ProductPrice>()
                .Where("productPrice.Price = 3");

            string sql = "";
            sql += "SELECT " + SELECT_PRICE_PRODUCT + ", ";
            sql += SELECT_PRODUCT_FOR_TEST.Replace("p.", "pr.").Replace("p_", "pr_") + " ";
            sql += "FROM product_price p ";
            sql += "LEFT JOIN product_for_test pr ON (pr.id = p.id_product) ";
            sql += "LEFT JOIN category_for_test c ON (c.id = pr.id_category) ";
            sql += "WHERE p.price = 3";

            Assert.That(targetPreco.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLComCondicaoCompostaPrecoProduto()
        {
            targetPreco = new QuerySimple<ProductPrice>()
                .Where("productPrice.Product.Name = 'Arroz'");

            string sql = "";
            sql += "SELECT " + SELECT_PRICE_PRODUCT + ", ";
            sql += SELECT_PRODUCT_FOR_TEST.Replace("p.", "pr.").Replace("p_", "pr_") + " ";
            sql += "FROM product_price p ";
            sql += "LEFT JOIN product_for_test pr ON (pr.id = p.id_product) ";
            sql += "LEFT JOIN category_for_test c ON (c.id = pr.id_category) ";
            sql += "WHERE pr.name = 'Arroz'";

            Assert.That(targetPreco.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void AssertSQLSimplesAlteracaoPrecoProduto()
        {
            targetAlteracao = new QuerySimple<AlteracaoPrecoProduto>();

            string sql = "";
            sql += "SELECT a.id a_id, a.data_alteracao a_data_alteracao, ";
            sql += SELECT_PRICE_PRODUCT + ", ";
            sql += SELECT_PRODUCT_FOR_TEST.Replace("p.", "pr.").Replace("p_", "pr_") + " ";
            sql += "FROM alteracao_preco_produto a ";
            sql += "LEFT JOIN product_price p ON (p.id = a.id_preco_prod) ";
            sql += "LEFT JOIN product_for_test pr ON (pr.id = p.id_product) ";
            sql += "LEFT JOIN category_for_test c ON (c.id = pr.id_category)";

            Assert.That(targetAlteracao.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void GeraSQLComCondicoesCompostasEOrderByCompostoAlteracaoPrecoProduto()
        {
            targetAlteracao = new QuerySimple<AlteracaoPrecoProduto>()
                .Where("AlteracaoPrecoProduto.PrecoProd.product.name = '058764' AND TRUNC(alteracaoPrecoProduto.DataAlteracao) > '11/07/2015' AND AlteracaoPrecoProduto.PrecoProd.Price < 4")
                .OrderBy("AlteracaoPrecoProduto.PrecoProd.InitDate, AlteracaoPrecoProduto.PrecoProd.product.Id ASC, alteracaoPrecoProduto.DataAlteracao DESC");

            string sql = "";
            sql += "SELECT a.id a_id, a.data_alteracao a_data_alteracao, ";
            sql += SELECT_PRICE_PRODUCT + ", ";
            sql += SELECT_PRODUCT_FOR_TEST.Replace("p.", "pr.").Replace("p_", "pr_") + " ";
            sql += "FROM alteracao_preco_produto a ";
            sql += "LEFT JOIN product_price p ON (p.id = a.id_preco_prod) ";
            sql += "LEFT JOIN product_for_test pr ON (pr.id = p.id_product) ";
            sql += "LEFT JOIN category_for_test c ON (c.id = pr.id_category) ";
            sql += "WHERE pr.name = '058764' ";
            sql += "AND TRUNC(a.data_alteracao) > '11/07/2015' ";
            sql += "AND p.price < 4 ";
            sql += "ORDER BY p.init_date, ";
            sql += "pr.id ASC, ";
            sql += "a.data_alteracao DESC";

            Assert.That(targetAlteracao.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void GeraSQLSimplesComBool()
        {
            var targetContato = new QuerySimple<Contato>()
                .Where("!contato.ativo");

            string sql = "";
            sql += "SELECT c.id c_id, c.ativo c_ativo, c.nome c_nome ";
            sql += "FROM contato c ";
            sql += "WHERE c.ativo = 0";

            Assert.That(targetContato.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void GeraSQLComBoolAtivoNoWhereEOrderBy()
        {
            var targetContato = new QuerySimple<Contato>()
                .Where("contato.ativo")
                .OrderBy("contato.ativo");

            string sql = "";
            sql += "SELECT c.id c_id, c.ativo c_ativo, c.nome c_nome ";
            sql += "FROM contato c ";
            sql += "WHERE c.ativo = 1 ";
            sql += "ORDER BY c.ativo";

            Assert.That(targetContato.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void GeraSQLComWhereCompostoComBoolEOrderBy()
        {
            var targetContato = new QuerySimple<Contato>()
                .Where("contato.nome LIKE '%!' AND !cOnTaTo.AtIvO")
                .OrderBy("contato.Id, contato.ativo, contato.NoMe");

            string sql = "";
            sql += "SELECT c.id c_id, c.ativo c_ativo, c.nome c_nome ";
            sql += "FROM contato c ";
            sql += "WHERE c.nome LIKE '%!' AND c.ativo = 0 ";
            sql += "ORDER BY c.id, c.ativo, c.nome";

            Assert.That(targetContato.ToString(), Is.EqualTo(sql));
        }

        [Test]
        public void GeraSQLComFiltrosEmDuasClassesComBoolean()
        {
            var targetPessoa = new QuerySimple<Pessoa>()
                .Where("!pessoa.contato.ativo AND pEsSoA.nome = 'NOME' AND pessoa.Ativo")
                .OrderBy("pessoa.contato.ativo, pessoa.contato.nome, PeSsoA.AtiVo");

            string sql = "";
            sql += "SELECT p.id p_id, p.ativo p_ativo, p.nome p_nome, ";
            sql += "c.id c_id, c.ativo c_ativo, c.nome c_nome ";
            sql += "FROM pessoa p ";
            sql += "LEFT JOIN contato c ON (c.id = p.id_contato) ";
            sql += "WHERE c.ativo = 0 AND p.nome = 'NOME' AND p.ativo = 1 ";
            sql += "ORDER BY c.ativo, c.nome, p.ativo";

            Assert.That(targetPessoa.ToString(), Is.EqualTo(sql));
        }
    }
}
