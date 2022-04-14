using System;
namespace KIT.Models
{
	public class SchemaExampleData : BaseKITEntity
	{
        public SchemaExampleData(string name, string content, string createdBy) : base(name, createdBy)
        {
            Content = content;
        }

		public string Content { get; set; }
    }
}

