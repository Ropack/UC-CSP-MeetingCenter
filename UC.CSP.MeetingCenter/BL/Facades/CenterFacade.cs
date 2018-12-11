using System;
using System.Collections.Generic;
using UC.CSP.MeetingCenter.BL.Queries;
using UC.CSP.MeetingCenter.BL.Repositories;
using UC.CSP.MeetingCenter.BL.Services;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Facades
{
    public class CenterFacade
    {
        // TODO: Change return values from entities to DTOs

        private IRepository<Center> CenterRepository { get; }
        private CsvImportService CsvImportService { get; }
        private CentersQuery CentersQuery { get; }
        public CenterFacade()
        {
            CenterRepository = new CenterRepository();
            CsvImportService = new CsvImportService(); 
            CentersQuery = new CentersQuery(); 
        }

        public Center GetById(int id)
        {
            return CenterRepository.GetById(id);
        }

        public Center GetByCode(string code)
        {
            return CenterRepository.GetByCode(code);
        }

        public void Create(Center entity)
        {
            CenterRepository.Create(entity);
        }

        public void Update(Center entity)
        {
            CenterRepository.Update(entity);
        }

        public void Delete(Center entity)
        {
            CenterRepository.Delete(entity);
        }

        public void ImportFromCsv(string filePath)
        {
            CsvImportService.Import(filePath);
        }

        public List<Center> GetAllCenters()
        {
            return CentersQuery.Execute();
        }
    }
}