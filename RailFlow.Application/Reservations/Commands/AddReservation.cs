using MediatR;

namespace RailFlow.Application.Reservations.Commands;

public record AddReservation(DateOnly Date, Guid UserId, Guid FirstScheduleId, Guid? SecondScheduleId, Guid StartStopId,
    TimeOnly StartHour, Guid EndStopId, TimeOnly EndHour, Guid? TransferStopId, long Price) : IRequest;