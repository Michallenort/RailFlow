namespace RailFlow.Application.Reservations.DTO;

public record ReservationDto(Guid Id, DateOnly Date, string StartStopName, TimeOnly StartHour, string EndStopName,
    TimeOnly EndHour, long Price);