namespace HomeServer.Services;

public interface ISessionService {
    public Guid userGuid { get; set; }
    public string IP { get; set; }
}