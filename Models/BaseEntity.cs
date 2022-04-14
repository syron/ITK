using System;
namespace KIT.Models
{
	public abstract class BaseKITEntity
	{        
        public BaseKITEntity(string name, string createdBy)
		{
            Name = name;
            Created = DateTimeOffset.UtcNow;
            CreatedBy = createdBy;
		}

        public BaseKITEntity(string name, string description, string createdBy)
        {
            Name = name;
            Description = description;
            Created = DateTimeOffset.UtcNow;
            CreatedBy = createdBy;
        }

        public BaseKITEntity(string name, string description, DateTimeOffset created, string createdBy)
        {
            Name = name;
            Description = description;
            Created = created;
            CreatedBy = createdBy;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }

        public string CreatedByLocalTime {
            get
            {
                return Created.ToLocalTime().ToString();
            }
        }
    }
}