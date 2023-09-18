namespace RailFlow.Application.Users.DTO;

public record UserDetailsDto(Guid Id, string FirstName, string LastName, string Email,
    DateTime? DateOfBirth, string Nationality, string RoleName);