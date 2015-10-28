using EntityJoke.Core;
using EntityJoke.Process;
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
    public class CommandDeleteGeneratorTest
    {

        CommandDeleteGenerator target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(CategoriaTeste));
        }

        [Test]
        public void GeraDeleteProduto()
        {
            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 65;
            cat.Nome = "Cat Delete";

            target = new CommandDeleteGenerator(cat);

            string delete = "DELETE FROM categoria_teste WHERE id = 65";

            Assert.That(target.GetCommand(), Is.EqualTo(delete));

        }

    }
}
