using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot.Helpers;
using Newtonsoft.Json;
using ADAM.DataClasses;
using static ff14bot.Helpers.Logging;
using ff14bot;
using System.IO;

namespace ADAM.DataHandlers
{
	internal class BuddyLog
	{
		internal static int _lastLogBuffer;
		internal static string _lastMessage;

		internal static void OnLogMessage(ReadOnlyCollection<Logging.LogMessage> logMessages)
		{
			/*foreach (LogMessage message in logMessages)
			{
				if (message.Message == _lastMessage) { continue; }
				BuddyLogMessage buddyLogMessage = new BuddyLogMessage();
				buddyLogMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
				buddyLogMessage.PlayerName = Core.Player.Name;
				buddyLogMessage.TimeStamp = message.Timestamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss"); ;
				buddyLogMessage.Color = message.Color.ToString();
				buddyLogMessage.Message = message.Message;

				Server.QueueMessage(JsonConvert.SerializeObject(buddyLogMessage));

				_lastLogBuffer = logMessages.Count;
				_lastMessage = message.Message;
			}*/
		}
	}
}
