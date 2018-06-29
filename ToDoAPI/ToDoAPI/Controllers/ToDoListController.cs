using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
	/// <summary>
	/// sets api route
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
		/// <summary>
		/// read only context
		/// </summary>
		private readonly ToDoContext _context;

		/// <summary>
		/// initializes list
		/// </summary>
		/// <param name="context">dbcontext input</param>
		public ToDoListController(ToDoContext context)
		{
			_context = context;

			if (_context.ToDoLists.Count() == 0)
			{
				_context.ToDoLists.Add(new ToDoList { Name = "List1" });
				_context.SaveChanges();
			}
		}

        /// <summary>
		/// method to get all list results
		/// </summary>
		/// <returns>list of lists</returns>
        [HttpGet]
        public ActionResult<List<ToDoList>> GetAll()
        {
			return _context.ToDoLists.ToList();
        }

		/// <summary>
		/// method to get single list from id
		/// </summary>
		/// <param name="id">integer of primary key to search for</param>
		/// <returns>item or not found</returns>
		[HttpGet("{id}", Name = "GetToDoList")]
		public ActionResult<ToDoList> GetByID(int id)
		{
			var toDoItem = _context.ToDoItems.Where(x => x.ListID == id).ToList();
			var item =  _context.ToDoLists.Find(id);

			if (item == null)
			{
				return NotFound();
			}

			item.ItemList = toDoItem;
			return item;
		}

		/// <summary>
		/// method to create a to do list
		/// </summary>
		/// <param name="item">item body to send</param>
		/// <returns>item to database</returns>
		[HttpPost]
		public IActionResult Create(ToDoList item)
		{
			_context.ToDoLists.Add(item);
			_context.SaveChanges();

			return CreatedAtRoute("GetToDoList", new { id = item.ID }, item);
		}

		/// <summary>
		/// method to update a list
		/// </summary>
		/// <param name="id">integer id primary key</param>
		/// <param name="item">body information to update with</param>
		/// <returns>ok or not found</returns>
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
			return Ok();
		}

		/// <summary>
		/// method to delete list
		/// </summary>
		/// <param name="id">integer primary key</param>
		/// <returns>not found or no content status</returns>
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var todo = _context.ToDoLists.Find(id);
			var todoItems = _context.ToDoItems.Where(x => x.ListID == id).ToList();
			if (todo == null)
			{
				return NotFound();
			}

			_context.ToDoLists.Remove(todo);

			foreach(var item in todoItems)
			{
				_context.ToDoItems.Remove(item);
			}
			
			_context.SaveChanges();
			return NoContent();
		}
	}
}

