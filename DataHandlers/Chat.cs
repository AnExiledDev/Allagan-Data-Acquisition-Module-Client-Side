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
	internal class Chat
	{
		private static int _lastBuffer = 0;

		internal static void ReadBufferEvent(object sender, EventArgs e)
		{
			try
			{
				List<ChatLogEntry> buffer = GamelogManager.CurrentBuffer;
				//List<ChatLogEntry> combatBuffer = new List<ChatLogEntry>();

				int bufferLength = buffer.Count;
				foreach (ChatLogEntry entry in buffer.Skip(_lastBuffer))
				{
					if (_lastBuffer > 0 && bufferLength > _lastBuffer)
					{
						//combatBuffer.Add(entry);
						DelegateMessage(entry);
						//Helper.LogToFile(entry.FullLine);
					}
					else
					{
						_lastBuffer = bufferLength;
						return;
					}
					
					//_lastBuffer = _lastBuffer + 1;
				}

				_lastBuffer = buffer.Count;
				//Helper.Log("Last Buffer " + _lastBuffer);

				//Combat.HandleCombatMessages(combatBuffer);
			}
			catch (Exception error)
			{
				Helper.LogToFile(error.ToString());
			}
		}

		internal static void DelegateMessage(ChatLogEntry entry)
		{
			string messageType = entry.MessageType.ToString();
			string message = entry.Contents;

			if (
				message.Contains("You are revived.") ||
				message.Contains("You are defeated") ||
				message.Contains(" equipped.") ||
				message.Contains(" unequipped.") ||
				message.Contains("You change to ") ||
				message.Contains("You attain level ") ||
				message.Contains(" items repaired.") ||
				message.Contains("You defeat") ||
				message.Contains("You sell ") ||
				message.Contains("you put up for sale in the")
			)
			{
				ChatMisc.HandleActionMessage(entry);
			}

			Match experienceInfo = null;
			experienceInfo = Regex.Match(message, @"You gain (\d*).* experience points");
			if (experienceInfo.Success)
			{
				ChatMisc.HandleExperienceMessage(entry, experienceInfo);
			}

			Match craftedInfo = null;
			craftedInfo = Regex.Match(message, @"You synthesize (a|an|\d*) (.*).");
			if (craftedInfo.Success)
			{
				ChatMisc.HandleCraftingMessage(entry, craftedInfo);
			}

			Match gatheredInfo = null;
			gatheredInfo = Regex.Match(message, @"You obtain (a|an|\d*) (.*).");
			if (gatheredInfo.Success)
			{
				ChatMisc.HandleGatheringMessage(entry, gatheredInfo);
			}

			Match fishGatheredInfo = null;
			fishGatheredInfo = Regex.Match(message, @"You land (a|an|\d*) (.*) measuring .*");
			if (fishGatheredInfo.Success)
			{
				ChatMisc.HandleFishGatheringMessage(entry, fishGatheredInfo);
			}

			Match marketSellInfo = null;
			marketSellInfo = Regex.Match(message, @"The (\d*) (.*) you put up for sale in the (.*) markets have sold for (\d*,\d*) gil .*");
			if (marketSellInfo.Success)
			{
				ChatMisc.HandleMarketSellMessage(entry, marketSellInfo);
			}

			Match itemSellInfo = null;
			itemSellInfo = Regex.Match(message, @"You sell (a|an|\d*) (.*) for (.*) gil.");
			if (itemSellInfo.Success)
			{
				ChatMisc.HandleItemSellMessage(entry, itemSellInfo);
			}

			Match itemPurchaseInfo = null;
			itemPurchaseInfo = Regex.Match(message, @"You purchase (a|an|\d*) (.*) for (.*) gil.");
			if (itemSellInfo.Success)
			{
				ChatMisc.HandleItemPurchaseMessage(entry, itemPurchaseInfo);
			}

			if (
				messageType == "27" ||
				messageType == "Party" ||
				messageType == "Say" ||
				messageType == "Shout" ||
				messageType == "Yell" ||
				messageType == "StandardEmotes" ||
				messageType == "Tell" ||
				messageType == "Tell_Receive" ||
				messageType == "Alliance" ||
				messageType == "FreeCompany" ||
				messageType == "Linkshell1" ||
				messageType == "Linkshell2" ||
				messageType == "Linkshell3" ||
				messageType == "Linkshell4" ||
				messageType == "Linkshell5" ||
				messageType == "Linkshell6" ||
				messageType == "Linkshell7" ||
				messageType == "Linkshell8" ||
				messageType == "CustomEmotes"
			)
			{
				HandleChatMessage(entry);
			}
		}

		internal static void HandleChatMessage(ChatLogEntry entry)
		{
			string messageType = entry.MessageType.ToString();
			string message = entry.Contents;
			string sender = "";

			try
			{
				if (string.IsNullOrWhiteSpace(entry.SenderDisplayName))
				{
					sender = Core.Me.Name;
				}
				else
				{
					sender = entry.SenderDisplayName;
				}
			}
			catch
			{
			}

			if (messageType == "27")
			{
				messageType = "Novice Network";
			}

			ChatMessage chatMessage = new ChatMessage();
			chatMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			chatMessage.PlayerName = Core.Player.Name;
			chatMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
			chatMessage.MessageType = messageType;
			chatMessage.Sender = sender;
			chatMessage.Message = Helper.EncodeNonAsciiCharacters(message);

			Server.QueueMessage(JsonConvert.SerializeObject(chatMessage));
		}
	}
}
