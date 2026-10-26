namespace HomeClient.ResponseDTOs;

public class BadRegisterResponse {
    public List<string>? Names { get; set; }
    public List<string>? Email { get; set; }
    public List<string>? Password { get; set; }
}