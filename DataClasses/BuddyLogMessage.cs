using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ADAM.DataClasses
{
	internal class BuddyLogMessage
	{
		public string Type = "BuddyLogMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string TimeStamp { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public string Color { get; set; }
		public string Message { get; set; }
	}
}
