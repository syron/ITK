using System;

namespace ITK.Web.Models
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

