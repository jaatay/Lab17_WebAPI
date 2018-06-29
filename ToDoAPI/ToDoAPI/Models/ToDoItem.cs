using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
	//model for a todo item
    public class ToDoItem
    {
		public int ID { get; set; }
		public string Name { get; set; }
		public bool IsComplete { get; set; }
		public int ListID { get; set; }
		public string ListName { get; set; }
    }
}
