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
		public bool ValidateTodo(TodoDTO todo, string method)
		{

			switch(method)
			{
				case "POST":
					if (!ValidateTodoText(todo.todo_text))
					{
						return false;
					}
				break;
				case "PUT":
					if (!ValidateTodoId(todo.id))
					{
						return false;
					}
					if (!ValidateTodoProperties(todo.todo_text, todo.completed))
					{
						return false;
					}
					break;
				default:
					if (!ValidateTodoId(todo.id))
					{
						return false;
					}
				break;
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

		public bool ValidateTodoText(string text)
		{
			if (text == "")
			{
				throw new Exception();
			}

			return true;
		}

		public bool ValidateTodoProperties(string text, bool ?completed)
		{
			if (text == "" && completed == null)
			{
				throw new Exception();
			}

			return true;
		}

	}
}
