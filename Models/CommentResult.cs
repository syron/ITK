using System;

namespace TabBlazor.Templates.Server.Models
{
	public class CommentResult
	{
		public CommentResult(string? message)
        {
			Message = message;
        }

        public string? Message { get; set; }
    }
}

