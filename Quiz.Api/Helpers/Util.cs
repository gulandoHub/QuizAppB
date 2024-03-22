using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace QuizApi.Helpers
{
    public static class Util
    {
        public static T DeSerializer<T>(XElement element) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(element.CreateReader());
        }

        public static T Deserialize<T>(string xml) where T : class
        {
            T obj;
            var xs = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(xml))
            {
                obj = (T)xs.Deserialize(stringReader);
            }

            return obj;
        }
    }
}
