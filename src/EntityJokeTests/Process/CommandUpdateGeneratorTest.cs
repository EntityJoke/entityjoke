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

        UpdateCommandGenerator target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesAspects.GetInstance().Clear();
        }

        [Test]
        public void GeraUpdateCategoriaTeste()
        {
            CategoryForTest categoria = new CategoryForTest();
            categoria.Id = 2;
            categoria.Name = "Comidas";

            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(categoria);

            categoria.Name = "Comidas 1";

            target = new UpdateCommandGenerator(categoria);

            string insert = "UPDATE category_for_test SET name = 'Comidas 1' WHERE id = 2 RETURNING ID";

            Assert.That(target.Generate(), Is.EqualTo(insert));
        }

        [Test]
        public void GeraUpdateProdutoTeste()
        {
            ProductForTest produto = new ProductForTest();
            produto.Id = 3;

            produto.Category = new CategoryForTest();
            produto.Category.Id = 4;
            produto.Category.Name = "Congelados";

            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(produto);

            target = new UpdateCommandGenerator(produto);
            Assert.That(target.Generate(), Is.EqualTo(""));

            produto.Category = null;
            produto.Name = "Lasanha";
            produto.Packing = "Caixa";
            produto.Maker = "Sadia";
            produto.Quantity = "650";
            produto.Unit = "g";

            target = new UpdateCommandGenerator(produto);

            string update = "";
            update += "UPDATE product_for_test ";
            update += "SET id_category = null, ";
            update += "maker = 'Sadia', ";
            update += "name = 'Lasanha', ";
            update += "packing = 'Caixa', ";
            update += "quantity = '650', ";
            update += "unit = 'g' ";
            update += "WHERE id = 3 RETURNING ID";

            Assert.That(target.Generate(), Is.EqualTo(update));
        }

        [Test]
        public void GeraUpdatePrecoProduto()
        {
            DateTime dataIni = new DateTime(2015, 11, 07);
            DateTime dataFim = new DateTime(2015, 11, 09);

            ProductPrice precoProduto = new ProductPrice();
            precoProduto.Id = 10;
            precoProduto.Price = 20;
            precoProduto.InitDate = dataIni;
            precoProduto.EndDate = dataFim;

            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(precoProduto);

            target = new UpdateCommandGenerator(precoProduto);
            Assert.That(target.Generate(), Is.EqualTo(""));

            precoProduto.Product = new ProductForTest();
            precoProduto.Product.Id = 4;
            precoProduto.Product.Name = "Trigo";

            target = new UpdateCommandGenerator(precoProduto);

            string update = "";
            update += "UPDATE product_price ";
            update += "SET id_product = 4 ";
            update += "WHERE id = 10 RETURNING ID";

            Assert.That(target.Generate(), Is.EqualTo(update));
        }

        [Test]
        public void GeraUpdateComparadorProduto()
        {
            DateTime data = new DateTime(2015, 11, 07);

            ProductsComparator comparador = new ProductsComparator();
            comparador.Id = 20;

            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(comparador);

            target = new UpdateCommandGenerator(comparador);
            Assert.That(target.Generate(), Is.EqualTo(""));

            comparador.ComparatorDate = data;

            comparador.ProductA = new ProductForTest();
            comparador.ProductA.Id = 4;
            comparador.ProductA.Name = "Trigo";

            comparador.ProductB = new ProductForTest();
            comparador.ProductB.Id = 23;
            comparador.ProductB.Name = "Macarrão";

            string update = "";
            update += "UPDATE products_comparator ";
            update += "SET comparator_date = '" + data.GetDateTimeFormats()[54] + "', ";
            update += "id_product_a = 4, ";
            update += "id_product_b = 23 ";
            update += "WHERE id = 20 RETURNING ID";

            target = new UpdateCommandGenerator(comparador);
            Assert.That(target.Generate(), Is.EqualTo(update));
        }

    }
}
