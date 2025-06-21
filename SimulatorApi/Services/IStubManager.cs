namespace SimulatorApi.Services;

public interface IStubManager
{
    Task UpdatedStubsAsync(string file);
}