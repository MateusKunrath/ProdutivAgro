namespace ProdutivAgro.Communication.Requests.Users;

public class RequestCreateUserJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid OrganizationId { get; set; }
}