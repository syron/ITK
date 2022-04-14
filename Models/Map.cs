using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIT.Models
{
    public class Map : BaseKITEntity
    {
        public Map(string name, MapType mapType, string createdBy)
            : base(name, createdBy)
        {
            MapType = mapType;
        }

        public MapType MapType { get; set; }
        public int InSchemaId { get; set; }
        [ForeignKey("InSchemaId")]
        public Schema? InSchema { get; set; }
        public int OutSchemaId { get; set; }
        [ForeignKey("OutSchemaId")]
        public Schema? OutSchema { get; set; }
        public string? Content { get; set; }
    }
}