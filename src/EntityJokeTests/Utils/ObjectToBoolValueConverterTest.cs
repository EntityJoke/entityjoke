using EntityJoke.Utils.Converters;
using NUnit.Framework;

namespace EntityJokeTests.Utils
{
    public class ObjectToBoolValueConverterTest
    {

        ObjectToBoolValueConverter target;

        [Test]
        public void Converte1ToTrue()
        {
            target = new ObjectToBoolValueConverter(1);

            Assert.That(target.Convert(), Is.True);
        }

        [Test]
        public void Converte0ToFalse()
        {
            target = new ObjectToBoolValueConverter(0);

            Assert.That(target.Convert(), Is.False);
        }

        [Test]
        public void ConverteBoolToFalse()
        {
            object obj = false; 
            target = new ObjectToBoolValueConverter(obj);

            Assert.That(target.Convert(), Is.False);
        }

        [Test]
        public void ConverteBoolToTrue()
        {
            object obj = true;
            target = new ObjectToBoolValueConverter(obj);

            Assert.That(target.Convert(), Is.True);
        }

    }
}
