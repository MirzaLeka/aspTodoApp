using BLL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementations
{
	public class TodoBLL : ITodoBLL
	{
		public bool ValidateTodo(TodoDTO todo)
		{
			if (!ValidateTodoId(todo.id))
			{
				return false;
			}

			if (!ValidateTodoProperties(todo.todo_text, todo.completed))
			{
				return false;
			}

			return true;
		}

		public bool ValidateTodoId(int ID)
		{
			if (ID < 1)
			{
				throw new ArgumentException();
			}

			return true;
		}

		public bool ValidateTodoProperties(string text, bool ?completed)
		{
			if (text == null && completed == null)
			{
				return false;
			}

			return true;
		}

	}
}
