using System;
namespace KIT.Models
{
	public sealed class Integration : BaseKITEntity
	{
        public Integration(string name, string createdBy)
            : base(name, createdBy)
		{
		}

		List<Map>? Maps { get; set; }
    }
}

