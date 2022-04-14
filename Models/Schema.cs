using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIT.Models
{
	public class Schema : BaseKITEntity
	{
        public Schema(string name, string createdBy)
                : base(name, createdBy)
		{
            DataType = DataType.JSON;
		}

        public Schema(string name, string definition, string createdBy) : base(name, createdBy)
        {
            DataType = DataType.JSON;
            Definition = definition;
        }

        public Schema(string name, string definition, DataType dataType, string createdBy) : base(name, createdBy)
        {
            DataType = DataType.JSON;
            Definition = definition;
            DataType = dataType;
        }

		public string? Definition { get; set; }
        public List<SchemaExampleData>? ExampleDatas { get; set; }
        public DataType DataType { get; set; }
        [NotMapped]
        public HistoryEntry? LatestChange { get; set; }
        public virtual ICollection<Map>? OutMaps { get; set; }
        public virtual ICollection<Map>? InMaps { get; set; }
    }
}

