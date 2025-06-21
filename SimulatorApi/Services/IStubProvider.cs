using SimulatorApi.Models;

namespace SimulatorApi.Services;

public interface IStubProvider
{
    Task<IEnumerable<StubRequest>> GetStubRequests();
}
