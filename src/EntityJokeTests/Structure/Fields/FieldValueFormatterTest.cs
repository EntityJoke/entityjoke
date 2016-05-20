using EntityJoke.Structure.Fields.Formaters;
using NUnit.Framework;
using System;

namespace EntityJokeTests.Structure.Fields
{
    public class FieldValueFormatterTest
    {

        IFieldValueFormatter target;

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void FormataCampoString()
        {
            var value = "String Value";
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("'String Value'"));
        }

        [Test]
        public void FormataCampoInt()
        {
            var value = 3;
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueNumberFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("3"));
        }

        [Test]
        public void FormataCampoLong()
        {
            var value = 999999999999999999L;
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueNumberFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("999999999999999999"));
        }

        [Test]
        public void FormataCampoULong()
        {
            var value = 9999999999999999999L;
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueNumberFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("9999999999999999999"));
        }

        [Test]
        public void FormataCampoDouble()
        {
            var value = 99999999.9999999;
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueNumberFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("99999999.9999999"));
        }

        [Test]
        public void FormataCampoFloat()
        {
            var value = 9999.999F;
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueNumberFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("9999.999"));
        }

        [Test]
        public void FormataCampoDecimal()
        {
            var value = 957865368421657.8546987651357M;
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueNumberFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("957865368421657.8546987651357"));
        }

        [Test]
        public void FormataCampoData()
        {
            var value = new DateTime(2015, 07, 11, 21, 25, 15, DateTimeKind.Utc);
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueDateFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("To_Timestamp(1436649915)"));
        }

        [Test]
        public void FormataCampoBoolean()
        {
            var value = true;
            target = FieldValueFormatterFactory.NewInstance(value);

            Assert.That(target, Is.InstanceOf(typeof(FieldValueBoolFormatterForSql)));
            Assert.That(target.Format(), Is.EqualTo("1"));
        }

    }
}
