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
        }

        [TearDown]
        public void TearDown()
        {
        }
        [Test]
        public void Number1CanInput()
        {
            Assert.That(DoubleValidateRule.IsCanInputKey("1"));
        }
        [Test]
        public void ACanNotInput()
        {
            var doubleInvalidaterule = new DoubleValidateRule();
            Assert.That(DoubleValidateRule.IsCanInputKey("A"), Is.EqualTo(false));
        }
        [Test]
        public void dotCanInputInJPCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
            Assert.That(DoubleValidateRule.IsCanInputKey("."), Is.EqualTo(true));
        }
        [Test]
        public void CommaCanInputInJPCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
            Assert.That(DoubleValidateRule.IsCanInputKey(","), Is.EqualTo(false));
        }
        [Test]
        public void dotCanNotInputInFRCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
            Assert.That(DoubleValidateRule.IsCanInputKey("."), Is.EqualTo(false));
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
        }
        [Test]
        public void CommaCanInputInFRCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
            Assert.That(DoubleValidateRule.IsCanInputKey(","), Is.EqualTo(true));
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
        }
        [Test]
        public void minusCanInput()
        {
            Assert.That(DoubleValidateRule.IsCanInputKey("-"), Is.EqualTo(true));
        }
        [Test]
        public void CanInputStringInJpCulture([Values("1.0","-","-10.00",".1","100.123456789")]string inputstring)
        {
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
            Assert.That(DoubleValidateRule.IsCanInputString(inputstring), Is.EqualTo(true));
        }
        [Test]
        public void CommaCanInputStringInFRCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
            Assert.That(DoubleValidateRule.IsCanInputString(","), Is.EqualTo(true));
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
        }
        [Test]
        public void CanNotInputString([Values("1.0.","--")]string inputstring)
        {
            Assert.That(DoubleValidateRule.IsCanInputString(inputstring), Is.EqualTo(false));
        }
    }

}
