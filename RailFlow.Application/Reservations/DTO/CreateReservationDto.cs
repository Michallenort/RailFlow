namespace RailFlow.Application.Reservations.DTO;

public record CreateReservationDto(Guid Id, DateOnly Date, Guid UserId, Guid FirstScheduleId,
    Guid? SecondScheduleId, Guid StartStopId, Guid EndStopId, Guid? TransferStopId, long Price);