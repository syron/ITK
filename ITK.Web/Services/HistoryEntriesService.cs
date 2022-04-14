using KIT.Models;
using System.Linq;

namespace KIT.Services;

public class HistoryEntriesService : IBaseService<HistoryEntry>
{
    private readonly AppDbContext _appDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HistoryEntriesService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
        _appDbContext = appDbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<List<HistoryEntry>?> GetAsync(int kitEntityId, KITEntityType type) 
    {
        var result = _appDbContext.HistoryEntries!
                                  .Where(he => he.KITEntityType == type && he.KITEntityId == kitEntityId)?
                                  .OrderByDescending(he => he.Created)
                                  .ToList();
        return Task.FromResult(result);
    }

    public Task<List<HistoryEntry>?> GetMultipleByIdsAsync(List<int> kitEntityIds, KITEntityType type)
    {
        var result = _appDbContext.HistoryEntries!
                                  .Where(he => kitEntityIds.Contains(he.KITEntityId) && he.KITEntityType == type)?
                                  .OrderByDescending(he => he.Created)
                                  .ToList();

        /*
        var result = _appDbContext.HistoryEntries!
                                  .Where(he => kitEntityIds.Contains(he.KITEntityId) && he.KITEntityType == type)?
                                  .GroupBy(he => he.Id, (x, y) => new { 
            Key = x, 
            Value = y.OrderByDescending(he => he.Created).First() 
        });
        */

        return Task.FromResult(result);
    }

    public Task<HistoryEntry?> GetLatestAsync(int kitEntityId, KITEntityType type)
    {
        Console.WriteLine(kitEntityId);
        var result = _appDbContext.HistoryEntries!
                                  .OrderByDescending(he => he.Created)
                                  .FirstOrDefault(he => he.KITEntityId == kitEntityId && he.KITEntityType == type);
        return Task.FromResult(result);
    }

    public async Task<HistoryEntry> AddAsync(HistoryEntry obj)
    {
        await _appDbContext.HistoryEntries!.AddAsync(obj, CancellationToken.None);
        await _appDbContext.SaveChangesAsync(CancellationToken.None);

        return obj;
    }

    public Task DeleteAsync(HistoryEntry obj)
    {
        throw new NotImplementedException();
    }

    public Task<HistoryEntry?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<HistoryEntry> UpdateAsync(HistoryEntry obj)
    {
        throw new NotImplementedException();
    }
}

