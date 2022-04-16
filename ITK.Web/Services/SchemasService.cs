using KIT.Models;
using KIT.Models.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KIT.Services;

public class SchemasService
{
    private readonly AppDbContext _appDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HistoryEntriesService _historyEntriesService;

    public SchemasService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, HistoryEntriesService historyEntriesService)
    {
        _appDbContext = appDbContext;
        _httpContextAccessor = httpContextAccessor;
        _historyEntriesService = historyEntriesService;

        if (_appDbContext.Schemas is null) {
            throw new NullReferenceException("Schemas is null.");
        }
    }

    public async Task<ServiceResponse<List<Schema>?>> GetSchemasAsync(int page = 1, int items = 25, DateTime? fromDate = null, DateTime? toDate = null, string? filter = null)
    {

        if (_appDbContext.Schemas is null)
            throw new Exception("Schemas in appdbcontext is null.");


        IQueryable<Schema>? schemasResult = _appDbContext.Schemas;
        if (toDate is not null && fromDate is not null)
        {
            schemasResult = schemasResult.Where(s => s.Created >= fromDate && s.Created <= toDate);
        }


        if (filter is not null)
            schemasResult = schemasResult
                                   .Where(s => s.Name!.Contains(filter) || (s.Description != null && s.Description.Contains(filter)))
                                   .Skip((page - 1) * items)
                                   .Include(s => s.ExampleDatas);
        else
            schemasResult = schemasResult
                                   .Skip((page-1) * items)
                                   .Include(s => s.ExampleDatas);

        List<Schema> schemas = schemasResult.ToList();

        if (schemas is not null)
        {
            var ids = schemas.Select(s => s.Id).ToList();

            var historyEvents = await _historyEntriesService.GetMultipleByIdsAsync(ids, KITEntityType.Schema);
            if (historyEvents is not null) {
                foreach (var schema in schemas)
                {
                    schema.LatestChange = historyEvents.FirstOrDefault(he => he.KITEntityId == schema.Id);
                }
            }
        }

        return new ServiceResponse<List<Schema>?>(schemas);
    }

    public Task<List<Schema>?> GetSchemasByDataTypeAsync(DataType dataType) 
    {
        return Task.FromResult(_appDbContext.Schemas!.Where(s => s.DataType == dataType)?.ToList());   
    }

    public async Task<Schema?> GetSchemaByIdAsync(int id)
    {
        var schema = await _appDbContext.Schemas!.FindAsync(id);

        if (schema is not null) {
            var latestChange = await _historyEntriesService.GetLatestAsync(id, KITEntityType.Schema);
            schema.LatestChange = latestChange;
        }
        return schema;
    }

    public async Task<Schema> AddAsync(Schema schema, string? comment = null)
    {
        string? username = string.Empty;
        if (_httpContextAccessor.HttpContext is not null) {
            foreach (var claim in _httpContextAccessor.HttpContext.User.Claims) {
                Console.WriteLine(claim); // id = http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
            }
            username = _httpContextAccessor.HttpContext.User?.Identity?.Name;
        }

        schema.Created = DateTimeOffset.UtcNow;

        await _appDbContext.Schemas!.AddAsync(schema, CancellationToken.None);
        await _appDbContext.SaveChangesAsync(CancellationToken.None);

        await _historyEntriesService.AddAsync(new HistoryEntry(schema.Id, KITEntityType.Schema, schema, username!, comment));

        return schema;
    }

    public async Task UpdateAsync(Schema schema, string? comment = null)
    {
        string? username = string.Empty;
        if (_httpContextAccessor.HttpContext is not null) {
            foreach (var claim in _httpContextAccessor.HttpContext.User.Claims) {
                Console.WriteLine(claim); // id = http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
            }
            username = _httpContextAccessor.HttpContext.User?.Identity?.Name;
        }

        //if (string.IsNullOrWhiteSpace(username)) 
        //{
        //    throw new Exception("Not authenticated");
        //}

        schema.Created = DateTimeOffset.UtcNow;

        _appDbContext.Schemas!.Update(schema);
        await _appDbContext.SaveChangesAsync(CancellationToken.None);

        await _historyEntriesService.AddAsync(new HistoryEntry(schema.Id, KITEntityType.Schema, schema, username!, comment));
    }

    public async Task AddExampleDataAsync(int schemaId, SchemaExampleData schemaExampleData)
    {
        string? username = string.Empty;
        if (_httpContextAccessor.HttpContext is not null)
        {
            foreach (var claim in _httpContextAccessor.HttpContext.User.Claims)
            {
                Console.WriteLine(claim); // id = http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
            }
            username = _httpContextAccessor.HttpContext.User?.Identity?.Name;
        }

        var schema = await GetSchemaByIdAsync(schemaId);

        if (schema is null)
        {
            throw new Exception($"Schema with Id '{schemaId}' not found.");
        }

        if (schema.ExampleDatas == null)
            schema.ExampleDatas = new List<SchemaExampleData>();

        schema.ExampleDatas.Add(schemaExampleData);

        string comment = $"New data added to schema. Name: {schemaExampleData.Name}";

        await _historyEntriesService.AddAsync(new HistoryEntry(schema.Id, KITEntityType.Schema, schema, username!, comment));
    }
}

