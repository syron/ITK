using KIT.Models;

namespace KIT.Services;

public class IntegrationsService
{
    private static readonly Integration[] Integrations = new[]
    {
        new Integration("INT001", "Robert"),
        new Integration("INT002", "Robert"),
        new Integration("INT003", "Robert")
    };

    public Task<Integration[]> GetIntegrationsAsync()
    {
        return Task.FromResult(Integrations);
    }
}

