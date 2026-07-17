namespace ProdutivAgro.Communication.Responses.Users;

public class ResponseUserProfileJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid OrganizationId { get; set; } = Guid.NewGuid();
}