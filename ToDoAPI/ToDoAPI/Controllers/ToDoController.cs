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
		/// <summary>
		/// private property to use ToDoContext in controller
		/// </summary>
		private readonly ToDoContext _context;

		/// <summary>
		/// initialize controller, if no items exist in to do list sets default item
		/// </summary>
		/// <param name="context"></param>
		public ToDoController(ToDoContext context)
		{
			_context = context;

			if (_context.ToDoItems.Count() == 0)
			{
				_context.ToDoItems.Add(new ToDoItem { Name = "Item1" });
				_context.SaveChanges();
			}
		}

		/// <summary>
		/// method to get all to do items
		/// </summary>
		/// <returns>list of all items</returns>
		[HttpGet]
		public ActionResult<List<ToDoItem>> GetAll()
		{

			return _context.ToDoItems.ToList();
		}

		/// <summary>
		/// method to get all items by id, and link with to do list
		/// </summary>
		/// <param name="id">integer of id to search for</param>
		/// <returns>requested item, or not found if no items</returns>
		[HttpGet("{id}", Name = "GetToDo")]
		public ActionResult<ToDoItem> GetByID(int id)
		{
			var item = _context.ToDoItems.FirstOrDefault(x=> x.ID == id);
			var itemList = _context.ToDoLists.FirstOrDefault(x => x.ID == item.ListID);


			if (item == null)
			{
				return NotFound();
			}

			if (item.ListID == 0)
			{
				item.ListName = "Not currently in a list.";
			} else
			{
				item.ListName = itemList.Name;
			}

			
			return item;
		}

		/// <summary>
		/// method to create a new item
		/// </summary>
		/// <param name="item">item to create, corresponding to body to pass in JSON</param>
		/// <returns>new item to database</returns>
		[HttpPost]
		public IActionResult Create(ToDoItem item)
		{
			_context.ToDoItems.Add(item);
			_context.SaveChanges();

			return CreatedAtRoute("GetToDo", new { id = item.ID }, item);
		}

		/// <summary>
		/// update an existing item
		/// </summary>
		/// <param name="id">integer primary key of item to be updated</param>
		/// <param name="item">body information to update item</param>
		/// <returns>not found if unsuccessful, or item to database</returns>
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
			todo.ListID = item.ListID;

			_context.ToDoItems.Update(todo);
			_context.SaveChanges();
			return Ok();
		}

		/// <summary>
		/// method to delete item
		/// </summary>
		/// <param name="id">integer primary key of item</param>
		/// <returns>not found or no content status</returns>
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