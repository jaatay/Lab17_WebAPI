using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoAPI.Models;

namespace TodoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDoController : ControllerBase
	{
		private readonly ToDoContext _context;

		public ToDoController(ToDoContext context)
		{
			_context = context;

			if (_context.ToDoItems.Count() == 0)
			{
				_context.ToDoItems.Add(new ToDoItem { Name = "Item1" });
				_context.SaveChanges();
			}
		}

		[HttpGet]
		public ActionResult<List<ToDoItem>> GetAll()
		{

			return _context.ToDoItems.ToList();
		}

		[HttpGet("{id}", Name = "GetToDo")]
		public ActionResult<ToDoItem> GetByID(int id)
		{
			var item = _context.ToDoItems.Find(id);
			if(item == null)
			{
				return NotFound();
			}
			return item;
		}

		[HttpPost]
		public IActionResult Create(ToDoItem item)
		{
			_context.ToDoItems.Add(item);
			_context.SaveChanges();

			return CreatedAtRoute("GetToDo", new { id = item.ID }, item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, ToDoItem item)
		{
			var todo = _context.ToDoItems.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			todo.IsComplete = item.IsComplete;
			todo.Name = item.Name;

			_context.ToDoItems.Update(todo);
			_context.SaveChanges();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var todo = _context.ToDoItems.Find(id);
			if(todo == null)
			{
				return NotFound();
			}

			_context.ToDoItems.Remove(todo);
			_context.SaveChanges();
			return NoContent();
		}
	}
}