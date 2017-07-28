using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataClasses
{
	internal class CraftingMessage
	{
		public string Type = "CraftingMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string TimeStamp { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public int Amount { get; set; }
		public string ItemName { get; set; }
	}
}
