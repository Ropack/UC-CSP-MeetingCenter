using UC.CSP.MeetingCenter.BL.Services;
using UC.CSP.MeetingCenter.DAL;

namespace UC.CSP.MeetingCenter.BL.Facades
{
    public class ApplicationFacade
    {
        private IStorageProvider StorageProvider { get; }
        public ApplicationFacade()
        {
            StorageProvider = new XmlFileStorageProvider();
        }
        public void LoadData()
        {
            StorageProvider.Load();
        }

        public void Save()
        {
            StorageProvider.Save();
            DatabaseContextFactory.GetContext().Changed = false;
        }

        public bool HasDataChanged()
        {
            return DatabaseContextFactory.GetContext().Changed;
        }
    }
}