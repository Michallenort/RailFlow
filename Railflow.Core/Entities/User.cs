namespace Railflow.Core.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public string Nationality { get; private set; }
    public string PasswordHash { get; private set; }
    
    public int RoleId { get; private set; }
    public Role Role { get; private set; }
    
    public ICollection<Reservation> Reservations { get; private set; }
    
    public ICollection<EmployeeAssignment> EmployeeAssignments { get; private set; }

    public User()
    {
        
    }
    
    public User(Guid id, string email, string firstName, string lastName, DateTime? dateOfBirth, string nationality, string passwordHash, int roleId)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
        PasswordHash = passwordHash;
        RoleId = roleId;
    }
    
    public void Update(string? email, string? firstName, string? lastName, DateTime? dateOfBirth)
    {
        Email = email ?? Email;
        FirstName = firstName ?? FirstName;
        LastName = lastName ?? LastName;
        DateOfBirth = dateOfBirth ?? DateOfBirth;
    }
}