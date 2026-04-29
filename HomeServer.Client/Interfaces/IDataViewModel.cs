namespace HomeServer.Client.Interfaces;

public interface IDataViewModel {
    string SaveFileName { get; init; }
    string ChangesFileName { get; init; }
    string? GetDataJson();
}