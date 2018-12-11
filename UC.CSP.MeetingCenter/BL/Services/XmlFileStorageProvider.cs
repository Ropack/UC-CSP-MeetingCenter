using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UC.CSP.MeetingCenter.DAL;

namespace UC.CSP.MeetingCenter.BL.Services
{
    public class XmlFileStorageProvider : IStorageProvider
    {
        // TODO: Move to config
        private readonly string fileName = "saved_data.xml";

        public void Save()
        {
            var context = DatabaseContextFactory.GetContext();
            XmlSerializer serializer = new XmlSerializer(context.GetType());
            var xml = "";
            using(var sw = new StreamWriter(fileName))
            using(var sww = new StringWriter())
            {
                using(XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings() {Indent = true}))
                {
                    serializer.Serialize(writer, context);
                    xml = sww.ToString();
                    sw.WriteLine(xml);
                }
            }
        }

        public void Load()
        {
            if (!File.Exists(fileName)) return;

            var context = DatabaseContextFactory.GetContext();
            XmlSerializer serializer = new XmlSerializer(context.GetType());
            using (var sr = new StreamReader(fileName))
            using (var sww = new StringReader(sr.ReadToEnd()))
            {
                DatabaseContextFactory.SetContext(serializer.Deserialize(sww) as IDatabaseContext);
            }
        }
    }
}