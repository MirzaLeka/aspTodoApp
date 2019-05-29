using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BLL.Interfaces;
using DAL;
using DTO;

namespace TodoApp.Controllers
{
    public class TodoController : ApiController
    {
		private readonly ITodoBLL _todoBLL;

		public TodoController(ITodoBLL todoBLL)
		{
			_todoBLL = todoBLL;
		}

        public IEnumerable<Todo> GetAllTodos()
        {
			using(TodoAppEntities entities = new TodoAppEntities()) {
				return entities.Todos.ToList();
			}
				
        }

		public IHttpActionResult GetOneTodo(int id)
		{
			string method = "GET";

			TodoDTO todoDto = new TodoDTO();
			todoDto.id = id;

			if (!_todoBLL.ValidateTodo(todoDto, method)) 
			{
				return BadRequest();
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
			string method = "POST";

			TodoDTO todoDto = new TodoDTO();
			todoDto.todo_text = todo.todo_text;

			if (!_todoBLL.ValidateTodo(todoDto, method))
			{
				return BadRequest();
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
			string method = "PUT";

			TodoDTO todoDto = new TodoDTO();
			todoDto.id = id;
			todoDto.todo_text = todo.todo_text;
			todoDto.completed = todo.completed;

			if (!_todoBLL.ValidateTodo(todoDto, method))
			{
				return BadRequest();
			}

			try
			{
				// everything below is DAL if else
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
						entity.completed = false;
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

			string method = "DELETE";

			TodoDTO todoDto = new TodoDTO();
			todoDto.id = id;

			if (!_todoBLL.ValidateTodo(todoDto, method))
			{
				return BadRequest();
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