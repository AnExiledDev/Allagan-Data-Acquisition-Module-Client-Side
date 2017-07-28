using System;
using ff14bot;
using ADAM.DataHandlers;
using ADAM.Translations;
using System.Threading;
using ff14bot.Helpers;
using System.IO;
using System.Windows.Media;
using Buddy.Overlay;
using ff14bot.Overlay3D;

namespace ADAM
{
	public class Adam
	{
		//private static System.Windows.Forms.Timer _readServerMessageTimer;
		private static System.Windows.Forms.Timer _readBufferTimer;
		private static System.Windows.Forms.Timer _getPlayerDataTimer;
		private static System.Windows.Forms.Timer _getPlayerBagsTimer;
		private static System.Windows.Forms.Timer _processQueueTimer;
		private static System.Windows.Forms.Timer _getBotBasesInformation;
		private static System.Windows.Forms.Timer _getPluginInformation;
		private static System.Windows.Forms.Timer _processCommandTimer;
		private static System.Windows.Forms.Timer _readServerMessageTimer;
		private static System.Windows.Forms.Timer _sendHeartbeatMessageTimer;

		private static Thread serverListenerThread;

		public void OnButtonPress()
		{
			SettingsWindow mainWindow = new SettingsWindow();
			mainWindow.Show();
		}

		public static void EnablePlugin()
		{
			if (String.IsNullOrEmpty(Settings.Instance.CoreAuthKey))
			{
				Helper.Log(Localization.Adam_EnablePlugin_TheAuthKeyForADAMIsNotSetPleaseSetTheAuthKeyThenRestartThePlugin);
			}
			else
			{
				Logging.OnLogMessage += BuddyLog.OnLogMessage;

				Server.ConnectToServer(Settings.Instance.CoreAuthKey);

				StartTimers();

				Helper.Log("Plugin loaded.");

				OrderBotProfiles.GetOrderBotProfiles();

				/*serverListenerThread = new Thread(Server.ReadServerMessage);
				serverListenerThread.Start();*/
			}
		}

		public static void DisablePlugin()
		{
			Server.DisconnectFromServer();

			StopTimers();

			Server.DisconnectFromServer();
			Helper.Log("Plugin unloaded.");
		}

		private static void StartTimers()
		{
			/*_readServerMessageTimer = new System.Windows.Forms.Timer();
			_readServerMessageTimer.Tick += new EventHandler(Server.ReadServerMessage);
			_readServerMessageTimer.Interval = 1000;
			_readServerMessageTimer.Start();*/

			_readBufferTimer = new System.Windows.Forms.Timer();
			_readBufferTimer.Tick += new EventHandler(Chat.ReadBufferEvent);
			_readBufferTimer.Interval = 1000;
			_readBufferTimer.Start();

			_getPlayerDataTimer = new System.Windows.Forms.Timer();
			_getPlayerDataTimer.Tick += new EventHandler(Character.GetPlayerDataEvent);
			_getPlayerDataTimer.Interval = 60000;
			_getPlayerDataTimer.Start();

			_getPlayerBagsTimer = new System.Windows.Forms.Timer();
			_getPlayerBagsTimer.Tick += new EventHandler(Inventory.GetInventoryDataEvent);
			_getPlayerBagsTimer.Interval = 60000;
			_getPlayerBagsTimer.Start();

			_processQueueTimer = new System.Windows.Forms.Timer();
			_processQueueTimer.Tick += new EventHandler(Server.ProcessQueue);
			_processQueueTimer.Interval = 100;
			_processQueueTimer.Start();

			_getBotBasesInformation = new System.Windows.Forms.Timer();
			_getBotBasesInformation.Tick += new EventHandler(BotData.GetBotBasesInformationEvent);
			_getBotBasesInformation.Interval = 10000;
			_getBotBasesInformation.Start();

			_getPluginInformation = new System.Windows.Forms.Timer();
			_getPluginInformation.Tick += new EventHandler(BotData.GetBotPluginsInformationEvent);
			_getPluginInformation.Interval = 60000;
			_getPluginInformation.Start();

			_processCommandTimer = new System.Windows.Forms.Timer();
			_processCommandTimer.Tick += new EventHandler(Command.ProcessCommandQueue);
			_processCommandTimer.Interval = 1000;
			_processCommandTimer.Start();

			_readServerMessageTimer = new System.Windows.Forms.Timer();
			_readServerMessageTimer.Tick += new EventHandler(Server.ReadServerMessage);
			_readServerMessageTimer.Interval = 1000;
			_readServerMessageTimer.Start();

			_sendHeartbeatMessageTimer = new System.Windows.Forms.Timer();
			_sendHeartbeatMessageTimer.Tick += new EventHandler(Server.SendHeartbeatMessage);
			_sendHeartbeatMessageTimer.Interval = 10000;
			_sendHeartbeatMessageTimer.Start();
		}

		private static void StopTimers()
		{
			try
			{
				//@_readServerMessageTimer.Dispose();
				@_readBufferTimer.Dispose();
				@_getPlayerDataTimer.Dispose();
				@_getPlayerBagsTimer.Dispose();
				@_processQueueTimer.Dispose();
				@_getBotBasesInformation.Dispose();
				@_getPluginInformation.Dispose();
				@_processCommandTimer.Dispose();
				@Server.ReconnectTimer.Dispose();
				@_readServerMessageTimer.Dispose();
				_sendHeartbeatMessageTimer.Dispose();

				@serverListenerThread.Abort();
			}
			catch (Exception e)
			{
			}
		}
	}
}
 