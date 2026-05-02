namespace HomeServer.Client.Interfaces;

public interface IDataViewModel {
    string ModelName { get; }
    string? GetDataJson();
}