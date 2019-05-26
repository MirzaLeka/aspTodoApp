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

		List<string> todos = new List<string>();


        public IEnumerable<Todo> GetAllTodos()
        {
			using(TodoAppEntities entities = new TodoAppEntities()) {
				return entities.Todos.ToList();
			}
				
        }

		public IHttpActionResult GetOneTodo(int id)
		{
			using(TodoAppEntities entities = new TodoAppEntities())
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
			try
			{
				using (TodoAppEntities entities = new TodoAppEntities())
				{
					entities.Todos.Add(todo);
					entities.SaveChanges();
					return Ok();
				}
			} catch(Exception ex)
			{
				return Content(HttpStatusCode.BadRequest, ex);
			}

		}

		[HttpPut]
		public IHttpActionResult UpdateTodo(int id, [FromBody] Todo todo)
		{
			try
			{
				using (TodoAppEntities entities = new TodoAppEntities())
				{
					var entity = entities.Todos.FirstOrDefault(tod => tod.id == id);

					if (entity == null)
					{
						return Content(HttpStatusCode.NotFound, "Todo not found");
					}

					entity.todo_text = todo.todo_text;

					if ((bool)todo.completed)
					{
						entity.completed = todo.completed;
						entity.completed_at = DateTime.Now;
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