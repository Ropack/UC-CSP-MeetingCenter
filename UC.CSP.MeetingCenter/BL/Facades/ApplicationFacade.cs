﻿using UC.CSP.MeetingCenter.BL.Services;

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
        }
    }
}