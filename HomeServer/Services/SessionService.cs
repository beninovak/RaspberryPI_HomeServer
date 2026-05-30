namespace HomeServer.Services;

public class SessionService : ISessionService {
    public Guid userGuid { get; set; }
    public string IP { get; set; }
}