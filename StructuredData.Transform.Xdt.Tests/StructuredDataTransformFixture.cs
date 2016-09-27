using System.Xml.Linq;
using NUnit.Framework;

namespace StructuredData.Transform.Xdt.Tests
{
    [TestFixture]
    public class StructuredDataTransformFixture
    {
        [Test]
        public void SimpleTransform()
        {
            var xml = new XElement("TestRoot", new XElement("Value", "Initial"));
            var transform = "<TestRoot xmlns:xdt=\"http://schemas.microsoft.com/XML-Document-Transform\"><Value xdt:Transform=\"Replace\">Replaced</Value></TestRoot>";
            var expected = new XElement("TestRoot", new XElement("Value", "Replaced"));
            var candidate = xml.ToString().Transform(transform, "text/xml", "xdt");
            Assert.That(XElement.Parse(candidate).ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}