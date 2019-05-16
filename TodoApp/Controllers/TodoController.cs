using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodoController : ApiController
    {

		List<string> todos = new List<string>();


        public IEnumerable<string> GetAllTodos()
        {
            return todos;
        }

		public string GetOneTodo(int id)
		{
			return todos[id];
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