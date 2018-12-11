using System.IO;
using Newtonsoft.Json;
using UC.CSP.MeetingCenter.DAL;

namespace UC.CSP.MeetingCenter.BL.Services
{
    public class JsonFileStorageProvider : IStorageProvider
    {
        // TODO: Move to config
        private readonly string fileName = "saved_data.json";

        public void Save()
        {
            var json = JsonConvert.SerializeObject(DatabaseContextFactory.GetContext(), Formatting.Indented);
            
            using (var sw = new StreamWriter(fileName))
            {
                sw.Write(json);
            }
        }

        public void Load()
        {
            if (!File.Exists(fileName)) return;
            
            var context = DatabaseContextFactory.GetContext();
            using (var sr = new StreamReader(fileName))
            {
                var json = sr.ReadToEnd();
                DatabaseContextFactory.SetContext(JsonConvert.DeserializeObject(json, context.GetType()) as IDatabaseContext);
            }
        }
    }
}