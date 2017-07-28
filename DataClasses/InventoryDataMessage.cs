using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataClasses
{
	internal class InventoryDataMessage
	{
		public string Type = "InventoryDataMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string TimeStamp { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public uint FreeSlots { get; set; }
		public InventoryItems InventoryItems { get; set; }
		public InventoryItems EquippedItems { get; set; }
		public InventoryItems ArmoryItems { get; set; }
		public InventoryItems Currency { get; set; }
		public InventoryItems Crystals { get; set; }
		public InventoryItems KeyItems { get; set; }
	}

	public class InventoryItems
	{
		public List<InventoryItem> Items { get; set; }
	}

	public class InventoryItem
	{
		public uint ItemID { get; set; }
		public string Name { get; set; }
		public bool IsHighQuality { get; set; }
		public uint Count { get; set; }
		public int Slot { get; set; }
		public float Condition { get; set; }
	}
}
