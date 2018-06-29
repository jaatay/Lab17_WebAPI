using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
		private readonly ToDoContext _context;

		public ToDoListController(ToDoContext context)
		{
			_context = context;

			if (_context.ToDoLists.Count() == 0)
			{
				_context.ToDoLists.Add(new ToDoList { Name = "List1" });
				_context.SaveChanges();
			}
		}
        // GET api/values
        [HttpGet]
        public ActionResult<List<ToDoList>> GetAll()
        {
			return _context.ToDoLists.ToList();
        }

		[HttpGet("{id}", Name = "GetToDoList")]
		public ActionResult<ToDoList> GetByID(int id)
		{
			var toDoItem = _context.ToDoItems.Where(x => x.ListID == id).ToList();
			var item =  _context.ToDoLists.Find(id);

			item.ItemList = toDoItem;
			if (item == null)
			{
				return NotFound();
			}
			return item;
		}

		[HttpPost]
		public IActionResult Create(ToDoList item)
		{
			_context.ToDoLists.Add(item);
			_context.SaveChanges();

			return CreatedAtRoute("GetToDoList", new { id = item.ID }, item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, ToDoList item)
		{
			var todo = _context.ToDoLists.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			
			todo.Name = item.Name;

			_context.ToDoLists.Update(todo);
			_context.SaveChanges();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var todo = _context.ToDoLists.Find(id);
			if (todo == null)
			{
				return NotFound();
			}

			_context.ToDoLists.Remove(todo);
			_context.SaveChanges();
			return NoContent();
		}
	}
}

