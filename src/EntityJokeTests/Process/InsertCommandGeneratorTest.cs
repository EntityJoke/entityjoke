using EntityJoke.Core;
using EntityJoke.Process.Commands;
using EntityJokeTests.Core;
using NUnit.Framework;
using System;

namespace EntityJokeTests.Process
{
    [TestFixture]
    public class InsertCommandGeneratorTest
    {

        InsertCommandGenerator target;

        [Test]
        public void GenerateInsertForPostgreSQLOfSimpleEntity()
        {
            CategoryForTest category = new CategoryForTest();
            category.Id = 2;
            category.Name = "Comidas";

            target = new InsertCommandGenerator(category);

            string insert = "INSERT INTO category_for_test (name) VALUES ('Comidas') RETURNING ID";

            Assert.That(target.Generate(), Is.EqualTo(insert));
        }

        [Test]
        public void GenerateInsertForPostgreSQLOfEntityWith1Join()
        {
            ProductForTest product = new ProductForTest();
            product.Id = 3;
            product.Name = "Lasanha";
            product.Packing = "Caixa";
            product.Maker = "Sadia";
            product.Quantity = "650";
            product.Unit = "g";

            product.Category = new CategoryForTest();
            product.Category.Id = 4;
            product.Category.Name = "Congelados";

            target = new InsertCommandGenerator(product);

            string insert = "";
            insert += "INSERT INTO product_for_test (id_category, maker, name, packing, quantity, unit) VALUES (4, 'Sadia', 'Lasanha', 'Caixa', '650', 'g') RETURNING ID";

            Assert.That(target.Generate(), Is.EqualTo(insert));
        }

        [Test]
        public void GenerateInsertForPostgreSQLOfEntityWithJoinNull()
        {
            ProductForTest product = new ProductForTest();
            product.Id = 3;
            product.Name = "Lasanha";
            product.Packing = "Caixa";
            product.Maker = "Sadia";
            product.Quantity = "650";
            product.Unit = "g";

            target = new InsertCommandGenerator(product);

            string insert = "";
            insert += "INSERT INTO product_for_test (maker, name, packing, quantity, unit) VALUES ('Sadia', 'Lasanha', 'Caixa', '650', 'g') RETURNING ID";

            Assert.That(target.Generate(), Is.EqualTo(insert));
        }

        [Test]
        public void GenerateInsertForPostgreSQLOfEntityWithDateTimeFields()
        {
            DateTime dateIni = new DateTime(2015, 11, 07);
            DateTime dateEnd = new DateTime(2015, 11, 09);

            ProductPrice priceProduct = new ProductPrice();
            priceProduct.Id = 10;
            priceProduct.Price = 20;
            priceProduct.InitDate = dateIni;
            priceProduct.EndDate = dateEnd;

            priceProduct.Product = new ProductForTest();
            priceProduct.Product.Id = 4;
            priceProduct.Product.Name = "Trigo";

            target = new InsertCommandGenerator(priceProduct);

            string insert = "";
            insert += "INSERT INTO product_price (end_date, init_date, price, id_product) VALUES ('" + dateEnd.GetDateTimeFormats()[54] + "', '" + dateIni.GetDateTimeFormats()[54] + "', 20, 4) RETURNING ID";

            Assert.That(target.Generate(), Is.EqualTo(insert));
        }

        [Test]
        public void GenerateInsertForPostgreSQLOfEntityWith2JoinsAndDateTime()
        {
            DateTime data = new DateTime(2015, 11, 07);

            ProductsComparator comparator = new ProductsComparator();
            comparator.Id = 20;
            comparator.ComparatorDate = data;

            comparator.ProductA = new ProductForTest();
            comparator.ProductA.Id = 4;
            comparator.ProductA.Name = "Trigo";

            comparator.ProductB = new ProductForTest();
            comparator.ProductB.Id = 23;
            comparator.ProductB.Name = "Macarrão";

            target = new InsertCommandGenerator(comparator);

            string insert = "";
            insert += "INSERT INTO products_comparator (comparator_date, id_product_a, id_product_b) VALUES ('" + data.GetDateTimeFormats()[54] + "', 4, 23) RETURNING ID";

            Assert.That(target.Generate(), Is.EqualTo(insert));
        }

    }
}
