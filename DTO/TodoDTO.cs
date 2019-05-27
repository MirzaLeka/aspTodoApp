using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class TodoDTO
	{
		public int id { get; set; }
		public string todo_text { get; set; }
		public Nullable<bool> completed { get; set; }
		public Nullable<System.DateTime> completed_at { get; set; }
	}
}
