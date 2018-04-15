using System;
using NUnit.Framework;
using NumericalUpDownSample;
using System.Globalization;

namespace NumericalUpDownTest
{
    [TestFixture]
    public class DoubleUpDownTest
    {
        private DoubleValidateRule doubleValidateRule_;
        [SetUp]
        public void SetUp()
        {
            doubleValidateRule_ = new DoubleValidateRule();
        }

        [TearDown]
        public void TearDown()
        {
            doubleValidateRule_ = null;
        }
        [Test]
        public void Number1CanInput()
        {
            Assert.That(doubleValidateRule_.IsCanInputKey("1"));
        }
        [Test]
        public void ACanNotInput()
        {
            var doubleInvalidaterule = new DoubleValidateRule();
            Assert.That(doubleValidateRule_.IsCanInputKey("A"), Is.EqualTo(false));
        }
        [Test]
        public void dotCanInputInJPCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
            Assert.That(doubleValidateRule_.IsCanInputKey("."), Is.EqualTo(true));
        }
        [Test]
        public void CommaCanInputInJPCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
            Assert.That(doubleValidateRule_.IsCanInputKey(","), Is.EqualTo(false));
        }
        [Test]
        public void dotCanNotInputInFRCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
            Assert.That(doubleValidateRule_.IsCanInputKey("."), Is.EqualTo(false));
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
        }
        [Test]
        public void CommaCanInputInFRCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
            Assert.That(doubleValidateRule_.IsCanInputKey(","), Is.EqualTo(true));
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
        }
        [Test]
        public void minusCanInputInFRCulture()
        {
            Assert.That(doubleValidateRule_.IsCanInputKey("-"), Is.EqualTo(true));
        }
    }

}
