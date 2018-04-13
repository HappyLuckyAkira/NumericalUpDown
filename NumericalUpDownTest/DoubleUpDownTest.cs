using System;
using NUnit.Framework;
using NumericalUpDownSample;
namespace NumericalUpDownTest
{
    [TestFixture]
    public class DoubleUpDownTest
    {
        [Test]
        public void Number1CanInput()
        {
            var doubleInvalidaterule = new DoubleValidateRule();
            Assert.That(doubleInvalidaterule.IsCanInputKey("1"));
        }
        [Test]
        public void ACanNotInput()
        {
            var doubleInvalidaterule = new DoubleValidateRule();
            Assert.That(doubleInvalidaterule.IsCanInputKey("A"), Is.EqualTo(false));
        }
    }
}
