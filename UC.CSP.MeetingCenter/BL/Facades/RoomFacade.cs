using UC.CSP.MeetingCenter.BL.Repositories;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Facades
{
    public class RoomFacade : FacadeBase
    {
        // TODO: Change return values from entities to DTOs

        private RoomRepository RoomRepository { get; }
        public RoomFacade()
        {
            RoomRepository = new RoomRepository();
        }

        public Room GetById(int id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                return RoomRepository.GetById(id);
            }
        }

        public Room GetByCode(string code)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                return RoomRepository.GetByCode(code);
            }
        }

        public void Create(Room entity)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                RoomRepository.Create(entity);
                uow.Commit();
            }
        }

        public void Update(Room entity)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                RoomRepository.Update(entity);
                uow.Commit();
            }
        }

        public void Delete(Room entity)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                RoomRepository.Delete(entity);
                uow.Commit();
            }
        }
    }
}