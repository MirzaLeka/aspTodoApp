using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using DAL;

namespace TodoApp.Controllers
{
    public class TodoController : ApiController
    {

        public IEnumerable<Todo> GetAllTodos()
        {
			using(TodoAppEntities entities = new TodoAppEntities()) {
				return entities.Todos.ToList();
			}
				
        }

		public IHttpActionResult GetOneTodo(int id)
		{
			if (id < 1)
			{
				throw new ArgumentException();
			}

			using (TodoAppEntities entities = new TodoAppEntities())
			{
				var entity = entities.Todos.FirstOrDefault(todo => todo.id == id);
				if (entity == null)
				{
					return Content(HttpStatusCode.NotFound, "Todo not found");
				}
				return Ok(entity);
			}
		} 

		[HttpPost]
		public IHttpActionResult AddTodo([FromBody] Todo todo)
		{

			if (todo.todo_text == null)
			{
				return Content(HttpStatusCode.BadRequest, "Todo requires text");
			}

			try
			{
				using (TodoAppEntities entities = new TodoAppEntities())
				{
					entities.Todos.Add(todo);
					entities.SaveChanges();
					return Ok(todo);
				}
			} catch(Exception ex)
			{
				return Content(HttpStatusCode.BadRequest, ex);
			}

		}

		[HttpPut]
		public IHttpActionResult UpdateTodo(int id, [FromBody] Todo todo)
		{

			if (id < 1)
			{
				throw new ArgumentException();
			}

			if (todo.todo_text == null && todo.completed == null)
			{
				return Content(HttpStatusCode.BadRequest, "Something went wrong");
			}

			try
			{
				using (TodoAppEntities entities = new TodoAppEntities())
				{
					var entity = entities.Todos.FirstOrDefault(tod => tod.id == id);

					if (entity == null)
					{
						return Content(HttpStatusCode.NotFound, "Todo not found");
					}

					if (todo.todo_text != null)
					{
						entity.todo_text = todo.todo_text;
					}


					if (todo.completed == true)
					{
						entity.completed = todo.completed;
						entity.completed_at = DateTime.Now;
					} else
					{
						entity.completed = todo.completed;
						entity.completed_at = null;
					}

					entities.SaveChanges();
					return Ok(entity);
					
				}

			} catch (Exception e)
			{
				return Content(HttpStatusCode.BadRequest, e);
			}
		}

		[HttpDelete]
		public IHttpActionResult DeleteTodo(int id)
		{

			if (id < 1)
			{
				throw new ArgumentException();
			}

			try
			{
				using(TodoAppEntities entities = new TodoAppEntities())
				{
					var entity = entities.Todos.FirstOrDefault(todo => todo.id == id);

					if (entity == null)
					{
						return Content(HttpStatusCode.NotFound, "Todo not found");
					}

					entities.Todos.Remove(entity);
					entities.SaveChanges();
					return Ok(entity);

				}
				
			} catch (Exception ex)
			{
				return Content(HttpStatusCode.BadRequest, ex);
			}
		}
		
    }
}