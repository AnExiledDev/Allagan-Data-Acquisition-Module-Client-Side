using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ADAM.DataHandlers;
using ff14bot;
using ff14bot.Managers;
using System.Threading;
using ADAM.DataClasses;
using Newtonsoft.Json;

namespace ADAM
{
	internal class Server
	{
		internal const string Protocol = "https://";
		internal const string Address = "allagandata.com";
		internal const string FullAddress = Protocol + Address;

		internal static TcpClient ClientSocket;
		internal static int ConnectionAttempts;

		internal static bool IsSendingMessage = false;
		internal static List<string> DataQueue = new List<string>();

		internal static System.Windows.Forms.Timer ReconnectTimer;

		internal static string ValidateKey(string key)
		{
			WebClient client = new WebClient();

			try
			{
				return client.DownloadString(FullAddress + "/ValidateKey?key=" + key);
			}
			catch
			{
				Helper.Log("Unable to validate key. ADAM's servers may be down.");

				return null;
			}
		}

		internal static bool IsSocketConnected()
		{
			try
			{
				if (ClientSocket.Client.Poll(0, SelectMode.SelectRead) == true && ClientSocket.Client.Available == 0)
				{
					BeginReconnect();
					return false;
				}
			}
			catch (Exception e)
			{
				BeginReconnect();

				return false;
			}

			return true;
		}

		internal static void ConnectToServer(string CoreAuthKey)
		{
			ClientSocket = new TcpClient();
			WebClient client = new WebClient();
			
			try
			{
				client.DownloadStringCompleted += (s, e) =>
				{
					try 
					{
						if (e.Error != null)
						{
							return;
						}

						if (!String.IsNullOrEmpty(e.Result))
						{
							if (e.Result.Length > 4 || e.Result.Length < 4)
							{
								Helper.Log("Unable to connect to ADAM's servers. Error returned: " + e.Result);
							}

							ClientSocket.Connect(Address, Convert.ToUInt16(e.Result));

							if (IsSocketConnected() && Core.Player != null)
							{
								Character.GetPlayerData();
								Inventory.GetInventoryData();
								BotData.GetBotBasesInformation();
								BotData.GetBotPluginsInformation();

								Helper.Log("A socket connection to ADAM's servers has been made successfully.");

								if (ReconnectTimer != null)
								{
									ReconnectTimer.Dispose();
									ReconnectTimer = null;
								}

								ConnectionAttempts = 0;
							}
						}
					}
					catch (Exception er)
					{
						Helper.Log("Unable to connect to ADAM's socket server. The servers may be down. Will attempt to reconnect in 30 seconds.");

						BeginReconnect();

						return;
					}
				};

				client.DownloadStringAsync(new Uri(FullAddress + "/AvailablePort/" + CoreAuthKey));
			}
			catch (Exception e)
			{
				Helper.Log("Unable to connect to ADAM's servers. The servers may be down. Will attempt to reconnect in 30 seconds.");

				BeginReconnect();

				return;
			}
		}

		internal static void DisconnectFromServer()
		{
			if (ClientSocket != null)
			{
				ClientSocket.Close();
			}
		}

		private static void ReconnectToServer()
		{
			ConnectionAttempts++;

			Helper.Log("Lost connection to ADAM servers! Attempting to reconnect, attempt #" + ConnectionAttempts);

			ConnectToServer(Settings.Instance.CoreAuthKey);
		}

		private static void ReconnectToServerEvent(object sender, EventArgs eventArgs)
		{
			ReconnectToServer();
		}

		private static void BeginReconnect()
		{
			if (ReconnectTimer == null)
			{
				ReconnectTimer = new System.Windows.Forms.Timer();
				ReconnectTimer.Tick += new EventHandler(ReconnectToServerEvent);
				ReconnectTimer.Interval = 25000 + new Random().Next(1000, 10000);
				ReconnectTimer.Start();
			}
		}

		internal static void QueueMessage(string message)
		{
			DataQueue.Add(message);
		}

		internal static void ProcessQueue(object sender, EventArgs eventArgs)
		{
			if (IsSendingMessage == true) 
			{
				return; 
			}

			if (DataQueue.Count <= 0)
			{
				return;
			}

			IsSendingMessage = true;
			SendServerMessage(DataQueue.First());
			IsSendingMessage = false;
		}

		internal static void SendHeartbeatMessage(object sender, EventArgs eventArgs)
		{
			HeartbeatMessage heartbeatMessage = new HeartbeatMessage();
			heartbeatMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			heartbeatMessage.PlayerName = Core.Player.Name;
			heartbeatMessage.TimeStamp = DateTime.UtcNow.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");

			QueueMessage(JsonConvert.SerializeObject(heartbeatMessage));
		}

		internal static void SendServerMessage(string message)
		{
			if (IsSocketConnected() == false)
			{
				return;
			}

			try
			{
				NetworkStream serverStream = ClientSocket.GetStream();
				byte[] outStream = Encoding.UTF8.GetBytes(message + "\\END");
				serverStream.Write(outStream, 0, outStream.Length);
				serverStream.Flush();

				//Helper.LogToFile("Sent Message: " + message);

				DataQueue.RemoveAt(0);
			}
			catch (Exception e)
			{

			}
		}

		internal static void ReadServerMessage(object sender, EventArgs eventArgs)
		{
			if (IsSocketConnected() == false)
			{
				return;
			}

			try
			{
				NetworkStream serverStream = ClientSocket.GetStream();

				if (serverStream.DataAvailable == false) {
					return;
				}

				byte[] data = new byte[1024];
				int receiveData = serverStream.Read(data, 0, data.Length);

				Command.QueueCommand(Encoding.UTF8.GetString(data, 0, receiveData));
			}
			catch (Exception e)
			{

			}
		}

		/*internal static void ReadServerMessage(object sender, EventArgs eventArgs)
		{
			if (IsSocketConnected() == false || Core.Player == null)
			{
				return;
			}

			try
			{
				WebClient client = new WebClient();

				client.DownloadStringCompleted += (s, e) =>
				{
					if (e.Error != null)
					{
						return;
					}

					if (!String.IsNullOrEmpty(e.Result) && e.Result[0] == '/')
					{
						SendChat(e.Result);
					}
				};

				client.DownloadStringAsync(new Uri(FullAddress + "/GetChatLine?CoreAuthKey=" + Settings.Instance.CoreAuthKey + "&PlayerName=" + Core.Player.Name));
			}
			catch (Exception e)
			{

			}
		}

		private static void SendChat(string message)
		{
			ChatManager.SendChat(message);
		}*/
	}
}
