using EntityJoke.Core;
using EntityJoke.Linq;
using EntityJoke.Structure;
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
    public class FieldPositionConditionExtractorTest
    {

        FieldPositionConditionExtractor target;

        [Test]
        public void AssertQueryNaoPossuiField()
        {
            string condition = "SELECT p.* FROM produto p WHERE produto.Id > 3";
            target = new FieldPositionConditionExtractor(condition);
            FieldPositionCondition position = target.Extract("produto.nome");

            Assert.That(position.Positions.Any(), Is.False);
        }

        [Test]
        public void AssertPosicaoComUmaOcorrenciaNoWhere()
        {
            string condition = "SELECT p.* FROM produto p WHERE produto.Id > 3";
            target = new FieldPositionConditionExtractor(condition);
            FieldPositionCondition position = target.Extract("produto.Id");

            Assert.That(position.Positions[0], Is.EqualTo(32));
        }

        [Test]
        public void AssertPosicaoComDuasOcorrenciasNoWhere()
        {
            string condition = "SELECT p.* FROM produto p WHERE produto.Id > 3 AND produto.Id < 8";
            target = new FieldPositionConditionExtractor(condition);
            FieldPositionCondition position = target.Extract("produto.Id");

            Assert.That(position.Positions[0], Is.EqualTo(51));
            Assert.That(position.Positions[1], Is.EqualTo(32));
        }

        [Test]
        public void AssertPosicaoComUmaOcorrenciaNoOrderBy()
        {
            string condition = "SELECT p.* FROM produto p Order By produto.Nome";
            target = new FieldPositionConditionExtractor(condition);
            FieldPositionCondition position = target.Extract("produto.Nome");

            Assert.That(position.Positions[0], Is.EqualTo(35));
        }

        [Test]
        public void AssertPosicaoComUmaOcorrenciaNoWhereEUmaNoOrderBy()
        {
            string condition = "SELECT p.* FROM produto p WHERE produto.Nome = 'Arroz' Order By produto.Nome";
            target = new FieldPositionConditionExtractor(condition);
            FieldPositionCondition position = target.Extract("produto.Nome");

            Assert.That(position.Positions[0], Is.EqualTo(64));
            Assert.That(position.Positions[1], Is.EqualTo(32));
        }

        [Test]
        public void AssertPosicaoComDuasOcorrenciasNoWhereEUmaNoOrderBy()
        {
            string condition = "SELECT p.* FROM produto p WHERE produto.Id > 3 AND produto.Id < 8 Order By produto.Id";
            target = new FieldPositionConditionExtractor(condition);
            FieldPositionCondition position = target.Extract("produto.Id");

            Assert.That(position.Positions[0], Is.EqualTo(75));
            Assert.That(position.Positions[1], Is.EqualTo(51));
            Assert.That(position.Positions[2], Is.EqualTo(32));
        }

        [Test]
        public void AssertPosicaoComTresOcorrenciasNoWhereEUmaNoOrderByComMinuscuroEMaisculo()
        {
            string condition = "SELECT p.* FROM produto p WHERE produto.id > 3 AND PRODUTO.ID < 8 Order By pRoDuTo.Id";
            target = new FieldPositionConditionExtractor(condition);
            FieldPositionCondition position = target.Extract("produto.Id");

            Assert.That(position.Positions[0], Is.EqualTo(75));
            Assert.That(position.Positions[1], Is.EqualTo(51));
            Assert.That(position.Positions[2], Is.EqualTo(32));
        }
    }
}
