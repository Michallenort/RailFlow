namespace RailFlow.Application.Trains.DTO;

public record TrainDto(Guid Id, int Number, float? MaxSpeed, int Capacity);