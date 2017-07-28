using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADAM.DataClasses;
using ff14bot;
using ff14bot.Helpers;
using Newtonsoft.Json;

namespace ADAM.DataHandlers
{
	class OrderBotProfiles
	{
		internal static void GetOrderBotProfiles()
		{
			try
			{
				List<OrderBotProfile> Profiles = new List<OrderBotProfile>();
				foreach (string FilePath in Directory.GetFiles(JsonSettings.AssemblyPath + "/Profiles", "*.xml", SearchOption.AllDirectories))
				{
					OrderBotProfile Profile = new OrderBotProfile();
					Profile.Name = FilePath.Split('/').Last().Replace(".xml", "");
					Profile.Path = FilePath;

					Profiles.Add(Profile);
				}

				OrderBotProfilesMessage orderBotProfilesMessage = new OrderBotProfilesMessage();
				orderBotProfilesMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
				orderBotProfilesMessage.PlayerName = Core.Player.Name;
				orderBotProfilesMessage.OrderBotProfiles = new DataClasses.OrderBotProfiles();
				orderBotProfilesMessage.OrderBotProfiles.Profiles = Profiles;

				Server.QueueMessage(JsonConvert.SerializeObject(orderBotProfilesMessage));
			}
			catch (Exception e)
			{
				Helper.LogToFile(e.ToString());
			}
		}
	}
}
