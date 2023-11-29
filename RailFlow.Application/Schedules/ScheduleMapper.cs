using RailFlow.Application.Assignments.DTO;
using RailFlow.Application.Routes.DTO;
using RailFlow.Application.Schedules.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Schedules;

public interface IScheduleMapper
{
    IEnumerable<ScheduleDto> MapScheduleDtos(IEnumerable<Schedule> schedules);
    ScheduleDetailsDto MapScheduleDetailsDto(Schedule schedule);
}

internal sealed class ScheduleMapper : IScheduleMapper
{
    public IEnumerable<ScheduleDto> MapScheduleDtos(IEnumerable<Schedule> schedules)
        => schedules.Select(x => new ScheduleDto(x.Id, x.Date, new RouteDto(
            x.Route.Id, x.Route.Name, x.Route.StartStation!.Name, 
            x.Route.EndStation!.Name, x.Route.Train!.Number, 
            x.Route.IsActive)));

    public ScheduleDetailsDto MapScheduleDetailsDto(Schedule schedule)
        => new(schedule.Id, schedule.Date, new RouteDto(
                schedule.Route.Id, schedule.Route.Name, 
                schedule.Route.StartStation!.Name, 
                schedule.Route.EndStation!.Name, 
                schedule.Route.Train!.Number, 
                schedule.Route.IsActive),
            schedule.EmployeeAssignments
                .Select(x => new AssignmentDto(
                    x.Id, x.User!.Email, x.ScheduleId, 
                    x.StartHour, 
                    x.EndHour)));
}