using ADAM.DataClasses;
using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataHandlers
{
	internal class BotData
	{
		internal static void GetBotBasesInformation()
		{
			if (Server.IsSocketConnected() == false || Core.Player == null)
			{
				return;
			}

			List<BotBase> botBasesHolderList = new List<BotBase>();
			foreach (ff14bot.AClasses.BotBase botBase in BotManager.Bots)
			{
				BotBase botBaseItem = new BotBase();
				botBaseItem.EnglishName = botBase.EnglishName;
				botBaseItem.Name = botBase.Name;
				botBaseItem.RequiresProfile = botBase.RequiresProfile;
				botBaseItem.IsSelected = (BotManager.Current == botBase) ? true : false;
				botBaseItem.IsRunning = (BotManager.Current == botBase) ? TreeRoot.IsRunning : false;

				botBasesHolderList.Add(botBaseItem);
			}
			
			LocalPlayer player = Core.Player;

			BotBotBasesMessage botBotBasesMessage = new BotBotBasesMessage();
			botBotBasesMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			botBotBasesMessage.PlayerName = player.Name;
			botBotBasesMessage.BotBases = new BotBases();
			botBotBasesMessage.BotBases.Bots = botBasesHolderList;

			Server.QueueMessage(JsonConvert.SerializeObject(botBotBasesMessage));
		}

		internal static void GetBotBasesInformationEvent(object sender, EventArgs e)
		{
			GetBotBasesInformation();
		}

		internal static void GetBotPluginsInformation()
		{
			if (Server.IsSocketConnected() == false || Core.Player == null)
			{
				return;
			}

			List<BotPlugin> botPluginHolderList = new List<BotPlugin>();
			foreach (PluginContainer botPlugin in PluginManager.Plugins)
			{
				BotVersion botVersion = new BotVersion();
				botVersion.Build = botPlugin.Plugin.Version.Build;
				botVersion.Major = botPlugin.Plugin.Version.Major;
				botVersion.Minor = botPlugin.Plugin.Version.Minor;
				botVersion.Revision = botPlugin.Plugin.Version.Revision;

				BotPlugin botPluginItem = new BotPlugin();
				botPluginItem.Author = botPlugin.Plugin.Author;
				botPluginItem.Description = botPlugin.Plugin.Description;
				botPluginItem.Name = botPlugin.Plugin.Name;
				botPluginItem.IsEnabled = botPlugin.Enabled;
				botPluginItem.Version = botVersion;

				botPluginHolderList.Add(botPluginItem);
			}

			LocalPlayer player = Core.Player;

			BotPluginsMessage botPluginsMessage = new BotPluginsMessage();
			botPluginsMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			botPluginsMessage.PlayerName = player.Name;
			botPluginsMessage.Plugins = new BotPlugins();
			botPluginsMessage.Plugins.Plugins = botPluginHolderList;

			Server.QueueMessage(JsonConvert.SerializeObject(botPluginsMessage));
		}

		internal static void GetBotPluginsInformationEvent(object sender, EventArgs e)
		{
			GetBotPluginsInformation();
		}
	}
}
