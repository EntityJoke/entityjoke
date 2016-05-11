using EntityJoke.Process.Commands;
using EntityJokeTests.EntidadesTestes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Process.Commands
{
    public class CommandSQLGeneratorTest
    {
        ICommandSQLGenerator target;

        [Test]
        public void CriaComandoInsert()
        {
            var novo = new Contato() { Nome = "Novo Contato", Ativo = true };
            target = CommandSQLGenerator.NewInstance(novo);

            Assert.That(target, Is.InstanceOf(typeof(CommandInsertGenerator)));
        }

        [Test]
        public void CriaComandoUpdate()
        {
            var novo = new Contato() { Id = 9, Nome = "Contato Antigo", Ativo = true };
            target = CommandSQLGenerator.NewInstance(novo);

            Assert.That(target, Is.InstanceOf(typeof(CommandUpdateGenerator)));
        }

    }
}
