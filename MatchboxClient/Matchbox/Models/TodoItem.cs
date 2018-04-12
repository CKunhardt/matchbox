using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Matchbox.Abstractions;

namespace Matchbox.Models
{
	public class TodoItem : TableData
	{
		public string Text { get; set; }
        public bool Complete { get; set; } 
	}
}

