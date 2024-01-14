using System.Xml.Serialization;

namespace Samples.Bridge.Implemetator
{
    public class XmlSerializator : Serializator
    {
        public override T Deserialize<T>(string text)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(text))
            {
                return serializer.Deserialize(reader) as T;
            }
        }

        public override string GetContentType()
        {
            return "xml";
        }

        public override string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }

}