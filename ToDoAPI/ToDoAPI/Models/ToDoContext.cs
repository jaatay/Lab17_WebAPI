using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Models
{
    public class ToDoContext: DbContext
    {
		/// <summary>
		/// db context, because context
		/// </summary>
		/// <param name="options">options to be used with DBContext</param>
		public ToDoContext(DbContextOptions<ToDoContext>options) : base(options)
		{

		}

		public DbSet<ToDoItem> ToDoItems { get; set; }
		public DbSet<ToDoList> ToDoLists { get; set; }
    }
}
