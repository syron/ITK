using System.Text.Json;

namespace KIT.Models;

public class HistoryEntry
{
    public HistoryEntry() 
    {
    }

    public HistoryEntry(int kitEntityId, KITEntityType kitEntityType, BaseKITEntity kitEntity, string createdBy)
    {
        KITEntityId = kitEntityId;
        KITEntityType = kitEntityType;
        KITEntitySerialized = JsonSerializer.Serialize(kitEntity);
        CreatedBy = createdBy;
        Created = DateTimeOffset.UtcNow;
    }

    public HistoryEntry(int kitEntityId, KITEntityType kitEntityType, BaseKITEntity kitEntity, string createdBy, string? comment)
    {
        KITEntityId = kitEntityId;
        KITEntityType = kitEntityType;
        KITEntitySerialized = JsonSerializer.Serialize(kitEntity);
        CreatedBy = createdBy;
        Created = DateTimeOffset.UtcNow;
        Comment = comment;
    }

    public int Id { get; set; }
    public string? Comment { get; set; }
    public int KITEntityId { get; set; }
    public KITEntityType KITEntityType { get; set; }
    public DateTimeOffset Created { get; set; }
    public string? KITEntitySerialized { get; set; }
    public string? CreatedBy { get; set; }
}