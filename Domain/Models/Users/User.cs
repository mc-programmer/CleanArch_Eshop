using Domain.Attributes;

namespace Domain.Models.Users;

[Auditable]
public class User
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
}