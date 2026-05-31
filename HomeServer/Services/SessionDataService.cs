using System.Net;

namespace HomeServer.Services;

public class SessionDataService : ISessionDataService {
    public Guid userGuid { get; set; }
    public IPAddress? IP { get; set; }
}