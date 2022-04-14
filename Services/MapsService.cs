using KIT.Models;
using Microsoft.EntityFrameworkCore;

namespace KIT.Services;

public class MapsService : IBaseService<Map>
{ 
    private readonly AppDbContext _appDbContext;

    public MapsService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        if (_appDbContext.Schemas is null) {
            throw new NullReferenceException("Schemas is null.");
        }
    }

    public Task<List<Map>> GetAsync(string? filter = null)
    {
         if (filter is not null)
            return Task.FromResult(_appDbContext.Maps!.Include(m => m.InSchema).Include(m => m.OutSchema).Where(m => m.Name!.Contains(filter) || (m.Description != null && m.Description.Contains(filter))).ToList());
        else
            return Task.FromResult(_appDbContext.Maps!.Include(m => m.InSchema).Include(m => m.OutSchema).ToList());
    }

    public async Task<Map> AddAsync(Map map)
    {
        map.Created = DateTimeOffset.UtcNow;

        await _appDbContext.Maps!.AddAsync(map);
        await _appDbContext.SaveChangesAsync();

        return map;
    }

    public async Task DeleteAsync(Map map)
    {
        _appDbContext.Maps!.Remove(map);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Map?> GetByIdAsync(int id)
    {
        return await _appDbContext.Maps!.FindAsync(id);
    }

    public Task<Map> UpdateAsync(Map obj)
    {
        throw new NotImplementedException();
    }
}

