using System.ComponentModel.Composition;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Web.XmlTransform;
using StructuredData.Transform.interfaces;

namespace StructuredData.Transform.xdt
{
    [Export(typeof(ITransformStructuredData))]
    [ExportMetadata("MimeType", "text/xml")]
    [ExportMetadata("Method", "xdt")]
    public class XmlXdtStructuredDataTransformer : ITransformStructuredData
    {
        public string Transform(string sourceData, string transformData)
        {
            using (var document = new XmlTransformableDocument())
            {
                document.PreserveWhitespace = true;
                document.LoadXml(sourceData);
                using (var transformation = new XmlTransformation(transformData, false, null))
                {
                    if (transformation.Apply(document))
                    {
                        return XElement.Load(new XmlNodeReader(document)).ToString();
                    }
                }
            }
            return null;
        }
    }
}