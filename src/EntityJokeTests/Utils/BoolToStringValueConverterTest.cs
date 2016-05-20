using EntityJoke.Utils.Converters;
using NUnit.Framework;

namespace EntityJokeTests.Utils
{
    public class BoolToStringValueConverterTest
    {

        BoolToStringValueConverter target;

        [Test]
        public void ConverteTrueTo1()
        {
            target = new BoolToStringValueConverter(true);

            Assert.That(target.Convert(), Is.EqualTo("1"));
        }

        [Test]
        public void ConverteFalseTo0()
        {
            target = new BoolToStringValueConverter(false);

            Assert.That(target.Convert(), Is.EqualTo("0"));
        }

    }
}
