namespace Railflow.Core.Entities;

public class User
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public string Nationality { get; private set; }
    public string PasswordHash { get; private set; }
    
    public int RoleId { get; private set; }
    public virtual Role Role { get; private set; }
}