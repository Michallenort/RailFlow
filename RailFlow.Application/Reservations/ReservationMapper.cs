using RailFlow.Application.Reservations.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Reservations;

public interface IReservationMapper
{
    IEnumerable<ReservationDto> MapReservationDtos(IEnumerable<Reservation> reservation);
}

internal sealed class ReservationMapper : IReservationMapper 
{
    public IEnumerable<ReservationDto> MapReservationDtos(IEnumerable<Reservation> reservation)
        => reservation.Select(x => new ReservationDto(x.Id, x.Date, x.StartStop.Station.Name,
            x.StartHour,x.EndStop.Station.Name, x.EndHour, x.Price));
}