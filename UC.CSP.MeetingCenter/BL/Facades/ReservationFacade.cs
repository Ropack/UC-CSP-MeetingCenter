using System;
using System.Collections.Generic;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.BL.Queries;
using UC.CSP.MeetingCenter.BL.Repositories;
using UC.CSP.MeetingCenter.DAL;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Facades
{
    public class ReservationFacade : FacadeBase
    {
        // TODO: Change return values from entities to DTOs

        private IRepository<Reservation> ReservationRepository { get; }
        public ReservationFacade()
        {
            ReservationRepository = new ReservationRepository();
        }

        public Reservation GetById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return ReservationRepository.GetById(id);
            }
        }

        public void Create(Reservation entity)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                ReservationRepository.Create(entity);
                uow.Commit();
            }
        }

        public void Update(Reservation entity)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                ReservationRepository.Update(entity);
                uow.Commit();
            }
        }

        public void Delete(Reservation entity)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                ReservationRepository.Delete(entity);
                uow.Commit();
            }
        }

        public List<Reservation> GetReservationsByRoomAndDate(int roomId, DateTime date)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var reservationFilterDTO = new ReservationFilterDTO()
                {
                    DateTime = date,
                    RoomId = roomId
                };
                return new ReservationsQuery(reservationFilterDTO).Execute();
            }
        }
    }
}