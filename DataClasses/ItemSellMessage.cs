using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataClasses
{
	internal class ItemSellMessage
	{
		public string Type = "ItemSellMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string TimeStamp { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public string ItemName { get; set; }
		public int Amount { get; set; }
		public int Gil { get; set; }
	}
}
