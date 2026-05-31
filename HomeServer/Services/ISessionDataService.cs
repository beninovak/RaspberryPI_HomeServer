using System.Net;

namespace HomeServer.Services;

public interface ISessionDataService {
    public Guid userGuid { get; set; }
    public IPAddress? IP { get; set; }
}