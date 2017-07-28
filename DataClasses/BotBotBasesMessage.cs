using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataClasses
{
	internal class BotBotBasesMessage
	{
		public string Type = "BotBotBasesMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public BotBases BotBases { get; set; }
	}

	public class BotBases
	{
		public List<BotBase> Bots { get; set; }
	}

	public class BotBase
	{
		public string EnglishName { get; set; }
		public string Name { get; set; }
		public bool RequiresProfile { get; set; }
		public bool IsSelected { get; set; }
		public bool IsRunning { get; set; }
	}
}
