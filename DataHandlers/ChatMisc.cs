using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ADAM.DataClasses;
using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using Newtonsoft.Json;

namespace ADAM.DataHandlers
{
	internal class ChatMisc
	{
		internal static void HandleActionMessage(ChatLogEntry entry)
		{
			string messageType = entry.MessageType.ToString();
			string message = entry.Contents;

			if (message.Contains("You are revived."))
			{
				messageType = "Death Message";
			}

			if (message.Contains(" equipped."))
			{
				messageType = "Gear Message";
			}

			if (message.Contains(" unequipped."))
			{
				messageType = "Gear Message";
			}

			if (message.Contains("You change to "))
			{
				messageType = "Class Change";
			}

			if (message.Contains("You attain level "))
			{
				messageType = "Level Up";
			}

			if (message.Contains(" items repaired."))
			{
				messageType = "Gear Message";
			}

			if (message.Contains("You defeat"))
			{
				messageType = "Kill Message";
			}

			if (message.Contains("You sell"))
			{
				messageType = "Sell Message";
			}

			if (message.Contains("you put up for sale in the"))
			{
				messageType = "Sell Message";
			}

			ActionMessage actionMessage = new ActionMessage();
			actionMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			actionMessage.PlayerName = Core.Player.Name;
			actionMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			actionMessage.MessageType = messageType;
			actionMessage.Message = Helper.EncodeNonAsciiCharacters(message);

			Server.QueueMessage(JsonConvert.SerializeObject(actionMessage));
		}

		internal static void HandleExperienceMessage(ChatLogEntry entry, Match experienceInfo)
		{
			//Helper.LogToFile("Full Line: " + entry.FullLine);
			//Helper.LogToFile(experienceInfo.Groups[1].Value);

			LocalPlayer player = Core.Player;

			ExperienceMessage experienceMessage = new ExperienceMessage();
			experienceMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			experienceMessage.PlayerName = player.Name;
			experienceMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			experienceMessage.Skill = player.CurrentJob.ToString();
			experienceMessage.ExpGained = Convert.ToInt32(experienceInfo.Groups[1].Value);

			Server.QueueMessage(JsonConvert.SerializeObject(experienceMessage));
		}

		internal static void HandleCraftingMessage(ChatLogEntry entry, Match craftedInfo)
		{
			string itemName = "";
			int amount = 0;
			if (craftedInfo.Groups[1].Value == "a" || craftedInfo.Groups[1].Value == "an")
			{
				amount = 1;
				itemName = craftedInfo.Groups[2].Value;
			}
			else
			{
				amount = Convert.ToInt32(craftedInfo.Groups[1].Value);
				itemName = craftedInfo.Groups[2].Value;
			}

			LocalPlayer player = Core.Player;
			CraftingMessage craftingMessage = new CraftingMessage();
			craftingMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			craftingMessage.PlayerName = player.Name;
			craftingMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			craftingMessage.Amount = amount;
			craftingMessage.ItemName = Helper.EncodeNonAsciiCharacters(itemName);

			Server.QueueMessage(JsonConvert.SerializeObject(craftingMessage));
		}

		internal static void HandleGatheringMessage(ChatLogEntry entry, Match gatheredInfo)
		{
			string itemName = "";
			int amount = 0;
			if (gatheredInfo.Groups[1].Value == "a" || gatheredInfo.Groups[1].Value == "an")
			{
				amount = 1;
				itemName = gatheredInfo.Groups[2].Value;
			}
			else
			{
				amount = Convert.ToInt32(gatheredInfo.Groups[1].Value.Replace(",", ""));
				itemName = gatheredInfo.Groups[2].Value;
			}

			GatheringMessage gatheringMessage = new GatheringMessage();
			gatheringMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			gatheringMessage.PlayerName = Core.Player.Name;
			gatheringMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			gatheringMessage.Amount = amount;
			gatheringMessage.ItemName = Helper.EncodeNonAsciiCharacters(itemName);

			Server.QueueMessage(JsonConvert.SerializeObject(gatheringMessage));
		}

		internal static void HandleFishGatheringMessage(ChatLogEntry entry, Match fishGatheredInfo)
		{
			string itemName = "";
			int amount = 0;
			if (fishGatheredInfo.Groups[1].Value == "a" || fishGatheredInfo.Groups[1].Value == "an")
			{
				amount = 1;
				itemName = fishGatheredInfo.Groups[2].Value;
			}
			else
			{
				amount = Convert.ToInt32(fishGatheredInfo.Groups[1].Value);
				itemName = fishGatheredInfo.Groups[2].Value;
			}

			GatheringMessage gatheringMessage = new GatheringMessage();
			gatheringMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			gatheringMessage.PlayerName = Core.Player.Name;
			gatheringMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			gatheringMessage.Amount = amount;
			gatheringMessage.ItemName = Helper.EncodeNonAsciiCharacters(itemName);

			Server.QueueMessage(JsonConvert.SerializeObject(gatheringMessage));
		}

		internal static void HandleMarketSellMessage(ChatLogEntry entry, Match marketSellInfo)
		{
			string itemName = "";
			int amount = 0;
			string location = "";
			int profit = 0;

			itemName = marketSellInfo.Groups[2].Value;
			amount = Convert.ToInt32(marketSellInfo.Groups[1].Value);
			location = marketSellInfo.Groups[3].Value;
			profit = Convert.ToInt32(marketSellInfo.Groups[4].Value.Replace(",", ""));

			MarketMessage marketMessage = new MarketMessage();
			marketMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			marketMessage.PlayerName = Core.Player.Name;
			marketMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			marketMessage.ItemName = Helper.EncodeNonAsciiCharacters(itemName);
			marketMessage.Amount = amount;
			marketMessage.Location = location;
			marketMessage.Profit = profit;

			Server.QueueMessage(JsonConvert.SerializeObject(marketMessage));
		}

		internal static void HandleItemSellMessage(ChatLogEntry entry, Match itemSellInfo)
		{
			string itemName = "";
			int amount = 0;
			int gil = 0;

			itemName = itemSellInfo.Groups[1].Value;
			amount = Convert.ToInt32(itemSellInfo.Groups[0].Value);
			gil = Convert.ToInt32(itemSellInfo.Groups[2].Value.Replace(",", ""));

			ItemSellMessage itemSellMessage = new ItemSellMessage();
			itemSellMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			itemSellMessage.PlayerName = Core.Player.Name;
			itemSellMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			itemSellMessage.ItemName = Helper.EncodeNonAsciiCharacters(itemName);
			itemSellMessage.Amount = amount;
			itemSellMessage.Gil = gil;

			Server.QueueMessage(JsonConvert.SerializeObject(itemSellMessage));
		}

		internal static void HandleItemPurchaseMessage(ChatLogEntry entry, Match itemPurchaseInfo)
		{
			string itemName = "";
			int amount = 0;
			int gil = 0;

			itemName = itemPurchaseInfo.Groups[1].Value;
			amount = Convert.ToInt32(itemPurchaseInfo.Groups[0].Value);
			gil = Convert.ToInt32(itemPurchaseInfo.Groups[2].Value.Replace(",", ""));

			ItemPurchaseMessage itemPurchaseMessage = new ItemPurchaseMessage();
			itemPurchaseMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			itemPurchaseMessage.PlayerName = Core.Player.Name;
			itemPurchaseMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			itemPurchaseMessage.ItemName = Helper.EncodeNonAsciiCharacters(itemName);
			itemPurchaseMessage.Amount = amount;
			itemPurchaseMessage.Gil = gil;

			Server.QueueMessage(JsonConvert.SerializeObject(itemPurchaseMessage));
		}
	}
}
