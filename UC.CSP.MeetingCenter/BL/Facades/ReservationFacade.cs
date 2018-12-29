using System;
using System.Collections.Generic;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.BL.Queries;
using UC.CSP.MeetingCenter.BL.Repositories;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Facades
{
    public class ReservationFacade
    {
        // TODO: Change return values from entities to DTOs

        private IRepository<Reservation> ReservationRepository { get; }
        public ReservationFacade()
        {
            ReservationRepository = new ReservationRepository();
        }

        public Reservation GetById(int id)
        {
            return ReservationRepository.GetById(id);
        }

        public void Create(Reservation entity)
        {
            ReservationRepository.Create(entity);
        }

        public void Update(Reservation entity)
        {
            ReservationRepository.Update(entity);
        }

        public void Delete(Reservation entity)
        {
            ReservationRepository.Delete(entity);
        }

        public List<Reservation> GetReservationsByRoomAndDate(int roomId, DateTime date)
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