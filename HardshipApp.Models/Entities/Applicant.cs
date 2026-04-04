namespace HardshipApp.Models.Entities;
public class Applicant
{
    public Guid Id {get; init;}
    public string FirstName{get; set;}
    public string LastName{get; set;}
    public DateOnly DateOfBirth{get; set;}
    public string Email{get; set;}
    public string? Phone{get; set;}
    public DateTime CreateAt{get; init;}
    public ICollection<HardshipApplication> HardshipApplications {get; set;} = new List<HardshipApplication>();
    private Applicant() { } //EF Core
    public Applicant(string firstName, string lastName, DateOnly dateOfBirth, string email, string? phone = null)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        Phone = phone;
        CreateAt = DateTime.UtcNow;
    }






}
