using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApp.Models
{
	public class Todo
	{
		public string text { get; set; }
		public bool completed { get; set; }
		public DateTime completedAt { get; set; }
	}
}