using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using StructuredData.Transform.xdt;

namespace StructuredData.Transform.Xdt.Tests
{
    [TestFixture]
    public class XmlXdtStructuredDataTransformerFixture
    {
        private string LoadEmbeddedResource(string file)
        {
            var array = Assembly.GetAssembly(typeof(XmlXdtStructuredDataTransformerFixture)).GetManifestResourceNames();
            var path = array.FirstOrDefault(s => s.EndsWith("." + file));
            if (path != null)
            {
                var stream = Assembly.GetAssembly(typeof(XmlXdtStructuredDataTransformerFixture)).GetManifestResourceStream(path);
                return stream == null ? null : new StreamReader(stream).ReadToEnd();
            }
            return null;
        }

        [Test]
        public void SimpleTransformation()
        {
            var xml = new XElement("TestRoot", new XElement("Value", "Initial"));
            var transform = "<TestRoot xmlns:xdt=\"http://schemas.microsoft.com/XML-Document-Transform\"><Value xdt:Transform=\"Replace\">Replaced</Value></TestRoot>";
            var expected = new XElement("TestRoot", new XElement("Value", "Replaced"));
            var candidate = new XmlXdtStructuredDataTransformer().Transform(xml.ToString(), transform);
            Assert.That(XElement.Parse(candidate).ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}