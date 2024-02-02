namespace Identity.Api;

public class TokenGenerationRequest
{
    public guid UserId { get; set; }
    public string Email { get; set; }
    public Dictionary<string, string> Claims { get; set; }
}
