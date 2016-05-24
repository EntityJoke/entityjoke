using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class EntitiesLoaderEntitiesWithJoinsTest
    {
        EntitiesLoader<ProductForTest> target;

        List<ProductForTest> products;
        DataTable productTable;

        [SetUp]
        public void SetUp()
        {
            SetUpDataTable();
            target = new EntitiesLoader<ProductForTest>(productTable);
            products = target.Load();
        }

        [Test]
        public void Load3Rows()
        {
            Assert.That(products.Count, Is.EqualTo(3));
        }

        [Test]
        public void AssertIdsLoaded()
        {
            Assert.That(products[0].Id, Is.EqualTo(1));
            Assert.That(products[1].Id, Is.EqualTo(2));
        }

        [Test]
        public void AssertNamesLoaded()
        {
            Assert.That(products[0].Name, Is.EqualTo("Trigo"));
            Assert.That(products[1].Name, Is.EqualTo("Arroz"));
        }

        [Test]
        public void AssertMakersLoaded()
        {
            Assert.That(products[0].Maker, Is.EqualTo("Alvalade"));
            Assert.That(products[1].Maker, Is.EqualTo("Tio João"));
        }

        [Test]
        public void AssertQuantitiesLoaded()
        {
            Assert.That(products[0].Quantity, Is.EqualTo("1"));
            Assert.That(products[1].Quantity, Is.EqualTo("500"));
        }

        [Test]
        public void AssertPackingsLoaded()
        {
            Assert.That(products[0].Packing, Is.EqualTo("Pacote"));
            Assert.That(products[1].Packing, Is.EqualTo("Pacote"));
        }

        [Test]
        public void AssertUnitsLoaded()
        {
            Assert.That(products[0].Unit, Is.EqualTo("Kg"));
            Assert.That(products[1].Unit, Is.EqualTo("g"));
        }

        [Test]
        public void AssertJoinLoaded()
        {
            Assert.That(products[0].Category.Id, Is.EqualTo(2));
            Assert.That(products[0].Category.Name, Is.EqualTo("Cereal 1"));

            Assert.That(products[1].Category.Id, Is.EqualTo(3));
            Assert.That(products[1].Category.Name, Is.EqualTo("Cereal 2"));
        }

        [Test]
        public void AssertThatAreEqualsCategories()
        {
            var prod1 = products[0];
            var prod2 = products[1];
            var prod3 = products[2];

            Assert.That(prod1.Category, Is.Not.EqualTo(prod2.Category));
            Assert.That(prod2.Category, Is.Not.EqualTo(prod3.Category));

            Assert.That(prod1.Category, Is.EqualTo(prod3.Category));
        }

        [Test]
        public void AssertThatAreEqualsCategoriesAndRefreshLaterNewQuery()
        {
            var prod1 = products[0];
            var prod2 = products[1];
            var prod3 = products[2];

            Assert.That(prod1.Category, Is.Not.EqualTo(prod2.Category));
            Assert.That(prod2.Category, Is.Not.EqualTo(prod3.Category));

            Assert.That(prod1.Category, Is.EqualTo(prod3.Category));

            Assert.That(prod1.Category.Name, Is.EqualTo("Cereal 1"));
            Assert.That(prod3.Category.Name, Is.EqualTo("Cereal 1"));

            productTable = new DataTable();
            SetUpColumns();
            AddRow(4, "Aveia", "Nestle", "500", "Pacote", "g", 2, "Cereal 1 Up");

            target = new EntitiesLoader<ProductForTest>(productTable);

            var prod4 = target.Load()[0];

            Assert.That(prod2.Category, Is.Not.EqualTo(prod1.Category));
            Assert.That(prod2.Category, Is.Not.EqualTo(prod3.Category));
            Assert.That(prod2.Category, Is.Not.EqualTo(prod4.Category));

            Assert.That(prod1.Category, Is.EqualTo(prod3.Category));
            Assert.That(prod1.Category, Is.EqualTo(prod4.Category));

            Assert.That(prod1.Category.Name, Is.EqualTo("Cereal 1 Up"));
            Assert.That(prod3.Category.Name, Is.EqualTo("Cereal 1 Up"));
            Assert.That(prod4.Category.Name, Is.EqualTo("Cereal 1 Up"));
        }

        private static void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.Clear();
            DictionaryEntitiesObjects.GetInstance().Clear();
        }

        private void SetUpDataTable()
        {
            productTable = new DataTable();
            SetUpColumns();
            AddRow(1, "Trigo", "Alvalade", "1", "Pacote", "Kg", 2, "Cereal 1");
            AddRow(2, "Arroz", "Tio João", "500", "Pacote", "g", 3, "Cereal 2");
            AddRow(3, "Farinha", "Campo Largo", "500", "Pacote", "g", 2, "Cereal 1");
        }

        private void SetUpColumns()
        {
            productTable.Columns.Add(new DataColumn("p_id"      , typeof(int)));
            productTable.Columns.Add(new DataColumn("p_name"    , typeof(string)));
            productTable.Columns.Add(new DataColumn("p_maker"   , typeof(string)));
            productTable.Columns.Add(new DataColumn("p_quantity", typeof(string)));
            productTable.Columns.Add(new DataColumn("p_packing" , typeof(string)));
            productTable.Columns.Add(new DataColumn("p_unit"    , typeof(string)));
            productTable.Columns.Add(new DataColumn("c_id"      , typeof(int)));
            productTable.Columns.Add(new DataColumn("c_name"    , typeof(string)));
        }

        private void AddRow(int id, string name, string maker, string quantity, string packing, string unit, int c_id, string c_name)
        {
            DataRow row1 = productTable.NewRow();
            row1["p_id"      ] = id;
            row1["p_name"    ] = name;
            row1["p_maker"   ] = maker;
            row1["p_quantity"] = quantity;
            row1["p_packing" ] = packing;
            row1["p_unit"    ] = unit;
            row1["c_id"      ] = c_id;
            row1["c_name"    ] = c_name;
            productTable.Rows.Add(row1);
        }
    }
}
