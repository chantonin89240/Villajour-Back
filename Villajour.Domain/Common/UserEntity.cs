namespace Villajour.Domain.Common;

public class UserEntity
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int Status { get; set; }
    public string? City { get; set; }
    public int PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public int Phone { get; set; }
}
