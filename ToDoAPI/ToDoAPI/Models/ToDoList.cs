using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
    public class ToDoList
    {
		public int ID { get; set; }
		public string Name { get; set; }
		public List<ToDoItem> ItemList { get; set; }
    }
}
