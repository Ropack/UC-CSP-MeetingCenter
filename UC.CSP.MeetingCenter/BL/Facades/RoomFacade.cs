using UC.CSP.MeetingCenter.BL.Repositories;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Facades
{
    public class RoomFacade
    {
        // TODO: Change return values from entities to DTOs

        private RoomRepository RoomRepository { get; }
        public RoomFacade()
        {
            RoomRepository = new RoomRepository();
        }

        public Room GetById(int id)
        {
            return RoomRepository.GetById(id);
        }

        public Room GetByCode(string code)
        {
            return RoomRepository.GetByCode(code);
        }

        public void Create(Room entity)
        {
            RoomRepository.Create(entity);
        }

        public void Update(Room entity)
        {
            RoomRepository.Update(entity);
        }

        public void Delete(Room entity)
        {
            RoomRepository.Delete(entity);
        }
    }
}