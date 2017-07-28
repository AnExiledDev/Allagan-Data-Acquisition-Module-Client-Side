using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataClasses
{
	internal class BotPluginsMessage
	{
		public string Type = "BotPluginsMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public BotPlugins Plugins { get; set; }
	}

	public class BotPlugins
	{
		public List<BotPlugin> Plugins { get; set; }
	}

	public class BotPlugin
	{
		public string Author { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public bool IsEnabled { get; set; }
		public BotVersion Version { get; set; }
	}

	public class BotVersion
	{
		public int Build { get; set; }
		public int Major { get; set; }
		public int Minor { get; set; }
		public int Revision { get; set; }
	}
}
