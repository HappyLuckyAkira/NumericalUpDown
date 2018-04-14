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
    }

}
