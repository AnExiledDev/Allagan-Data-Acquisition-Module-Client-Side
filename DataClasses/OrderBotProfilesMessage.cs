using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataClasses
{
	internal class OrderBotProfilesMessage
	{
		public string Type = "OrderBotProfilesMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public OrderBotProfiles OrderBotProfiles { get; set; }
	}

	internal class OrderBotProfiles
	{
		public List<OrderBotProfile> Profiles { get; set; }
	}

	internal class OrderBotProfile
	{
		public string Name { get; set; }
		public string Path { get; set; }
	}
}
