using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Reservations.Commands.Handlers;

internal sealed class AddReservationHandler : IRequestHandler<AddReservation>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IValidator<AddReservation> _validator;
    
    public AddReservationHandler(IReservationRepository reservationRepository, 
        IValidator<AddReservation> validator, IUserRepository userRepository, 
        IScheduleRepository scheduleRepository, IStopRepository stopRepository)
    {
        _reservationRepository = reservationRepository;
        _validator = validator;
        _userRepository = userRepository;
        _scheduleRepository = scheduleRepository;
        _stopRepository = stopRepository;
    }
    
    public async Task Handle(AddReservation request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        if (await _userRepository.GetByIdAsync(request.UserId) is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        if (await _scheduleRepository.GetByIdAsync(request.FirstScheduleId) is null)
        {
            throw new ScheduleNotFoundException(request.FirstScheduleId);
        }
        
        if (request.SecondScheduleId is not null && await _scheduleRepository.GetByIdAsync(request.SecondScheduleId.Value) is null)
        {
            throw new ScheduleNotFoundException(request.SecondScheduleId.Value);
        }
        
        if (await _stopRepository.GetByIdAsync(request.StartStopId) is null)
        {
            throw new StopNotFoundException(request.StartStopId);
        }
        
        if (await _stopRepository.GetByIdAsync(request.EndStopId) is null)
        {
            throw new StopNotFoundException(request.EndStopId);
        }
        
        if (request.TransferStopId is not null && await _stopRepository.GetByIdAsync(request.TransferStopId.Value) is null)
        {
            throw new StopNotFoundException(request.TransferStopId.Value);
        }
        
        var reservation = new Reservation(Guid.NewGuid(), request.Date, request.UserId, request.FirstScheduleId, 
            request.SecondScheduleId, request.StartStopId, request.StartHour, request.EndStopId, request.EndHour, request.TransferStopId, request.Price);
        
        await _reservationRepository.AddAsync(reservation);
    }
}