using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.BL.Queries;
using UC.CSP.MeetingCenter.BL.Repositories;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Facades
{
    public class AccessoryFacade : FacadeBase
    {
        private AccessoryRepository AccessoryRepository { get; }
        private IRepository<StockOperation> StockOperationRepository { get; }
        public AccessoryFacade()
        {
            AccessoryRepository = new AccessoryRepository();
            StockOperationRepository = new RepositoryBase<StockOperation>();
        }
        public AccessoryDTO GetById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var entity = AccessoryRepository.GetById(id);
                if (entity != null)
                {
                    return Mapper.Map<AccessoryDTO>(entity);
                }

                return null;
            }
        }

        public void Save(AccessoryDTO accessory)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var e = AccessoryRepository.GetById(accessory.Id);
                var entity = Mapper.Map<Accessory>(accessory);
                if (e == null)
                {
                    AccessoryRepository.Create(entity);
                }
                else
                {
                    AccessoryRepository.Update(entity);
                }
                uow.Commit();
            }
        }
        public List<AccessoryDTO> GetAll()
        {
            using (UnitOfWorkProvider.Create())
            {
                return new AccessoriesQuery().Execute();
            }
        }
        public List<AccessoryDTO> GetByCategory(int categoryId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return new AccessoriesQuery()
                {
                    CategoryId = categoryId
                }.Execute();
            }
        }
        public List<CategoryDTO> GetCategories()
        {
            using (UnitOfWorkProvider.Create())
            {
                return new CategoriesQuery().Execute();
            }
        }

        public void Delete(AccessoryDTO accessory)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var entity = AccessoryRepository.GetById(accessory.Id);
                if (entity != null)
                {
                    AccessoryRepository.Delete(entity);
                    uow.Commit();
                }
            }
        }

        public void Issue(AccessoryStockDTO accessory)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                accessory.Accessory.StoredCount -= accessory.Count;
                var entity = Mapper.Map<Accessory>(accessory.Accessory);
                AccessoryRepository.Update(entity);
                //StockOperationRepository.Create(new StockOperation()
                //{
                //    AccessoryId = entity.Id,
                //    Count = accessory.Count,
                //    DateTime = DateTime.UtcNow
                //});
                StockOperationRepository.Create(Mapper.Map<StockOperation>(accessory));
                uow.Commit();
            }
        }

        public void Receipt(AccessoryStockDTO accessory)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                accessory.Accessory.StoredCount += accessory.Count;
                var entity = Mapper.Map<Accessory>(accessory.Accessory);
                AccessoryRepository.Update(entity);
                uow.Commit();
            }
        }
    }
}