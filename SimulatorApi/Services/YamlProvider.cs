using SimulatorApi.Models;

namespace SimulatorApi.Services;

public class YamlProvider : IStubProvider
{
    public Task<IEnumerable<StubRequest>> GetStubRequests()
    {
        throw new NotImplementedException();
    }
}