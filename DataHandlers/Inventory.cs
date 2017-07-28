using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADAM.DataClasses;
using ff14bot;
using ff14bot.Enums;
using ff14bot.Managers;
using ff14bot.Objects;
using Newtonsoft.Json;

namespace ADAM.DataHandlers
{
	internal class Inventory
	{
		internal static void GetInventoryData()
		{
			if (Server.IsSocketConnected() == false || Core.Player == null)
			{
				return;
			}

			List<BagSlot> inventoryBags = new List<BagSlot>();
			List<BagSlot> equippedItems = new List<BagSlot>();
			List<BagSlot> armoryItems = new List<BagSlot>();
			List<BagSlot> currency = new List<BagSlot>();
			List<BagSlot> crystals = new List<BagSlot>();
			List<BagSlot> keyItems = new List<BagSlot>();

			Bag bag1 = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Bag1);
			foreach (BagSlot bag in bag1) { inventoryBags.Add(bag); }

			Bag bag2 = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Bag2);
			foreach (BagSlot bag in bag2) { inventoryBags.Add(bag); }

			Bag bag3 = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Bag3);
			foreach (BagSlot bag in bag3) { inventoryBags.Add(bag); }

			Bag bag4 = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Bag4);
			foreach (BagSlot bag in bag4) { inventoryBags.Add(bag); }

			Bag equipped = InventoryManager.GetBagByInventoryBagId(InventoryBagId.EquippedItems);
			foreach (BagSlot bag in equipped) { equippedItems.Add(bag); }

			Bag armoryMainHand = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_MainHand);
			foreach (BagSlot bag in armoryMainHand) { armoryItems.Add(bag); }

			Bag armoryOffHand = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_OffHand);
			foreach (BagSlot bag in armoryOffHand) { armoryItems.Add(bag); }

			Bag armoryHelmet = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Helmet);
			foreach (BagSlot bag in armoryHelmet) { armoryItems.Add(bag); }

			Bag armoryChest = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Chest);
			foreach (BagSlot bag in armoryChest) { armoryItems.Add(bag); }

			Bag armoryGlove = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Glove);
			foreach (BagSlot bag in armoryGlove) { armoryItems.Add(bag); }

			Bag armoryBelt = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Belt);
			foreach (BagSlot bag in armoryBelt) { armoryItems.Add(bag); }

			Bag armoryPants = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Pants);
			foreach (BagSlot bag in armoryPants) { armoryItems.Add(bag); }

			Bag armoryBoots = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Boots);
			foreach (BagSlot bag in armoryBoots) { armoryItems.Add(bag); }

			Bag armoryEarrings = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Earrings);
			foreach (BagSlot bag in armoryEarrings) { armoryItems.Add(bag); }

			Bag armoryNecklace = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Necklace);
			foreach (BagSlot bag in armoryNecklace) { armoryItems.Add(bag); }

			Bag armoryWrits = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Writs);
			foreach (BagSlot bag in armoryWrits) { armoryItems.Add(bag); }

			Bag armoryRings = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Rings);
			foreach (BagSlot bag in armoryRings) { armoryItems.Add(bag); }

			Bag armorySouls = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Armory_Souls);
			foreach (BagSlot bag in armorySouls) { armoryItems.Add(bag); }

			Bag currencyBag = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Currency);
			foreach (BagSlot bag in currencyBag) { currency.Add(bag); }

			Bag crystalsBag = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Crystals);
			foreach (BagSlot bag in crystalsBag) { crystals.Add(bag); }

			Bag keyItemsBag = InventoryManager.GetBagByInventoryBagId(InventoryBagId.KeyItems);
			foreach (BagSlot bag in keyItemsBag) { keyItems.Add(bag); }

			InventoryItems inventoryItemsHolder = new InventoryItems();
			List<InventoryItem> inventoryItemsHolderList = new List<InventoryItem>();
			foreach (BagSlot bagSlot in inventoryBags)
			{
				if (String.IsNullOrEmpty(bagSlot.Name) == false)
				{
					InventoryItem inventoryItem = new InventoryItem();
					inventoryItem.ItemID = bagSlot.RawItemId;
					inventoryItem.Name = bagSlot.Name;
					inventoryItem.IsHighQuality = bagSlot.IsHighQuality;
					inventoryItem.Count = bagSlot.Count;
					inventoryItem.Slot = bagSlot.Slot;
					inventoryItem.Condition = bagSlot.Condition;

					inventoryItemsHolderList.Add(inventoryItem);
				}
			}
			inventoryItemsHolder.Items = inventoryItemsHolderList;

			InventoryItems equippedItemsHolder = new InventoryItems();
			List<InventoryItem> equippedItemsHolderList = new List<InventoryItem>();
			foreach (BagSlot bagSlot in equippedItems)
			{
				if (String.IsNullOrEmpty(bagSlot.Name) == false)
				{
					InventoryItem inventoryItem = new InventoryItem();
					inventoryItem.ItemID = bagSlot.RawItemId;
					inventoryItem.Name = bagSlot.Name;
					inventoryItem.IsHighQuality = bagSlot.IsHighQuality;
					inventoryItem.Count = bagSlot.Count;
					inventoryItem.Slot = bagSlot.Slot;
					inventoryItem.Condition = bagSlot.Condition;

					equippedItemsHolderList.Add(inventoryItem);
				}
			}
			equippedItemsHolder.Items = equippedItemsHolderList;

			InventoryItems armoryItemsHolder = new InventoryItems();
			List<InventoryItem> armoryItemsHolderList = new List<InventoryItem>();
			foreach (BagSlot bagSlot in armoryItems)
			{
				if (String.IsNullOrEmpty(bagSlot.Name) == false)
				{
					InventoryItem inventoryItem = new InventoryItem();
					inventoryItem.ItemID = bagSlot.RawItemId;
					inventoryItem.Name = bagSlot.Name;
					inventoryItem.IsHighQuality = bagSlot.IsHighQuality;
					inventoryItem.Count = bagSlot.Count;
					inventoryItem.Slot = bagSlot.Slot;
					inventoryItem.Condition = bagSlot.Condition;

					armoryItemsHolderList.Add(inventoryItem);
				}
			}
			armoryItemsHolder.Items = armoryItemsHolderList;

			InventoryItems currencyItemsHolder = new InventoryItems();
			List<InventoryItem> currencyItemsHolderList = new List<InventoryItem>();
			foreach (BagSlot bagSlot in currency)
			{
				if (String.IsNullOrEmpty(bagSlot.Name) == false)
				{
					InventoryItem inventoryItem = new InventoryItem();
					inventoryItem.ItemID = bagSlot.RawItemId;
					inventoryItem.Name = bagSlot.Name;
					inventoryItem.IsHighQuality = bagSlot.IsHighQuality;
					inventoryItem.Count = bagSlot.Count;
					inventoryItem.Slot = bagSlot.Slot;
					inventoryItem.Condition = bagSlot.Condition;

					currencyItemsHolderList.Add(inventoryItem);
				}
			}
			currencyItemsHolder.Items = currencyItemsHolderList;

			InventoryItems crystalsItemsHolder = new InventoryItems();
			List<InventoryItem> crystalsItemsHolderList = new List<InventoryItem>();
			foreach (BagSlot bagSlot in crystals)
			{
				if (String.IsNullOrEmpty(bagSlot.Name) == false)
				{
					InventoryItem inventoryItem = new InventoryItem();
					inventoryItem.ItemID = bagSlot.RawItemId;
					inventoryItem.Name = bagSlot.Name;
					inventoryItem.IsHighQuality = bagSlot.IsHighQuality;
					inventoryItem.Count = bagSlot.Count;
					inventoryItem.Slot = bagSlot.Slot;
					inventoryItem.Condition = bagSlot.Condition;

					crystalsItemsHolderList.Add(inventoryItem);
				}
			}
			crystalsItemsHolder.Items = crystalsItemsHolderList;

			InventoryItems keyItemsItemsHolder = new InventoryItems();
			List<InventoryItem> keyItemsItemsHolderList = new List<InventoryItem>();
			foreach (BagSlot bagSlot in keyItems)
			{
				if (String.IsNullOrEmpty(bagSlot.Name) == false)
				{
					InventoryItem inventoryItem = new InventoryItem();
					inventoryItem.ItemID = bagSlot.RawItemId;
					inventoryItem.Name = bagSlot.Name;
					inventoryItem.IsHighQuality = bagSlot.IsHighQuality;
					inventoryItem.Count = bagSlot.Count;
					inventoryItem.Slot = bagSlot.Slot;
					inventoryItem.Condition = bagSlot.Condition;

					keyItemsItemsHolderList.Add(inventoryItem);
				}
			}
			keyItemsItemsHolder.Items = keyItemsItemsHolderList;

			LocalPlayer player = Core.Player;

			InventoryDataMessage inventoryDataMessage = new InventoryDataMessage();
			inventoryDataMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			inventoryDataMessage.PlayerName = player.Name;
			inventoryDataMessage.FreeSlots = InventoryManager.GetBagByInventoryBagId(InventoryBagId.Bag1).FreeSlots + InventoryManager.GetBagByInventoryBagId(InventoryBagId.Bag2).FreeSlots + InventoryManager.GetBagByInventoryBagId(InventoryBagId.Bag3).FreeSlots + InventoryManager.GetBagByInventoryBagId(InventoryBagId.Bag4).FreeSlots;
			inventoryDataMessage.InventoryItems = inventoryItemsHolder;
			inventoryDataMessage.EquippedItems = equippedItemsHolder;
			inventoryDataMessage.ArmoryItems = armoryItemsHolder;
			inventoryDataMessage.Currency = currencyItemsHolder;
			inventoryDataMessage.Crystals = crystalsItemsHolder;
			inventoryDataMessage.KeyItems = keyItemsItemsHolder;

			Server.QueueMessage(JsonConvert.SerializeObject(inventoryDataMessage).Replace("\\", ""));
		}

		internal static void GetInventoryDataEvent(object sender, EventArgs e)
		{
			GetInventoryData();
		}
	}
}
