using ff14bot;
using ff14bot.AClasses;
using ff14bot.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataHandlers
{
	internal class Command
	{
		internal static List<string> CommandQueue = new List<string>();

		internal static void QueueCommand(string commandData)
		{
			string[] allCommands = commandData.Split('}');

			foreach (string command in allCommands)
			{
				if (command.Length <= 1) { continue; }
				CommandQueue.Add(command + "}");
				//Helper.Log("Queued Command: " + command + "}");
			}
		}

		internal static void ProcessCommandQueue(object sender, EventArgs eventArgs)
		{
			if (CommandQueue.Count <= 0)
			{
				return;
			}

			DelegateCommand(CommandQueue.First());
			CommandQueue.RemoveAt(0);
		}
		
		internal static void DelegateCommand(string commandData)
		{
			CommandMessage commandMessage = JsonConvert.DeserializeObject<CommandMessage>(commandData);

			if (commandMessage.Type == "SendChat")
			{
				ChatManager.SendChat(commandMessage.Command.Replace(@"\\", @"\"));
			}

			if (commandMessage.Type == "SetBotBase")
			{
				BotBase botbase = BotManager.Bots.Where(f => f.Name == commandMessage.Command).First();
				BotManager.SetCurrent(botbase);

				BotData.GetBotBasesInformation();
			}

			if (commandMessage.Type == "StartBotBase")
			{
				TreeRoot.Start();

				BotData.GetBotBasesInformation();
			}

			if (commandMessage.Type == "StopBotBase")
			{
				TreeRoot.Stop();

				BotData.GetBotBasesInformation();
			}

			if (commandMessage.Type == "EnablePlugin")
			{
				PluginContainer plugin = PluginManager.Plugins.Where(f => f.Plugin.Name == commandMessage.Command).First();
				plugin.Enabled = true;

				BotData.GetBotPluginsInformation();
			}

			if (commandMessage.Type == "DisablePlugin")
			{
				PluginContainer plugin = PluginManager.Plugins.Where(f => f.Plugin.Name == commandMessage.Command).First();
				plugin.Enabled = false;

				BotData.GetBotPluginsInformation();
			}
		}
	}

	internal class CommandMessage
	{
		public string Type { get; set; }
		public string Command { get; set; }
	}
}
