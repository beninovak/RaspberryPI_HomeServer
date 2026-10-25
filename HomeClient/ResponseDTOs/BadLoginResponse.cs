namespace HomeClient.ResponseDTOs;

public class BadLoginResponse {
    public List<string>? Email { get; set; }
    public List<string>? Password { get; set; }
    public List<string>? General { get; set; }
}