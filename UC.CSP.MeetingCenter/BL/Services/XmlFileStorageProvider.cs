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
            var subReq = DatabaseContextFactory.GetContext();
            XmlSerializer xsSubmit = new XmlSerializer(subReq.GetType());
            var xml = "";
            using(var sw = new StreamWriter(fileName))
            using(var sww = new StringWriter())
            {
                using(XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings() {Indent = true}))
                {
                    xsSubmit.Serialize(writer, subReq);
                    xml = sww.ToString();
                    sw.WriteLine(xml);
                }
            }
        }

        public void Load()
        {
            if (!File.Exists(fileName)) return;
            
            
        }
    }
}