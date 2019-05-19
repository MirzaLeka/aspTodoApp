using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DAL;

namespace TodoApp.Controllers
{
    public class TodoController : ApiController
    {

		List<string> todos = new List<string>();


        public IEnumerable<Todo> GetAllTodos()
        {
			using(TodoAppEntities entities = new TodoAppEntities()) {
				return entities.Todos.ToList();
			}
				
        }

		public Todo GetOneTodo(int id)
		{
			using(TodoAppEntities entities = new TodoAppEntities())
			{
				return entities.Todos.FirstOrDefault(todo => todo.id == id);
			}
		} 

		public IHttpActionResult AddTodo([FromBody] string text)
		{
			todos.Add(text);
			return Ok();
		}

		public IHttpActionResult UpdateTodo(int id, [FromBody] string newValue)
		{
			todos[id] = newValue;
			return Ok();
		}

		public IHttpActionResult DeleteTodo(int id)
		{
			todos.RemoveAt(id);
			return Ok();
		}
		
    }
}