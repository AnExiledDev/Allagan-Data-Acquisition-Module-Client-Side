using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataClasses
{
	internal class CombatMessage
	{
		public string Type = "CombatMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string TimeStamp { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public string Attacker { get; set; }
		public string Attackee { get; set; }
		public string Class { get; set; }
		public string Spell { get; set; }
		public int Damage { get; set; }
		public bool IsMiss { get; set; }
		public bool IsAutoAttack { get; set; }
		public bool IsCriticalAttack { get; set; }
	}
}
