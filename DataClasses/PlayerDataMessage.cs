using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAM.DataClasses
{
	internal class PlayerDataMessage
	{
		public string Type = "PlayerDataMessage";
		public string CoreAuthKey { get; set; }
		public string PlayerName { get; set; }
		public string TimeStamp { get; set; }
		public string Version = AdamLoader.CurrentVersion;
		public string CurrentJob { get; set; }
		public string Location { get; set; }
		public uint IdLocation { get; set; }
		public uint FateId { get; set; }
		public uint CurrentHealth { get; set; }
		public uint MaxHealth { get; set; }
		public uint CurrentMana { get; set; }
		public uint MaxMana { get; set; }
		public short CurrentTP { get; set; }
		public short MaxTP { get; set; }
		public uint CurrentExperience { get; set; }
		public uint CurrentRestedExperience { get; set; }
		public uint ExperienceRequired { get; set; }
		public ushort GladiatorLevel { get; set; }
		public ushort PugilistLevel { get; set; }
		public ushort MarauderLevel { get; set; }
		public ushort LancerLevel { get; set; }
		public ushort ArcherLevel { get; set; }
		public ushort ConjurerLevel { get; set; }
		public ushort ThaumaturgeLevel { get; set; }
		public ushort CarpenterLevel { get; set; }
		public ushort BlacksmithLevel { get; set; }
		public ushort ArmorerLevel { get; set; }
		public ushort GoldsmithLevel { get; set; }
		public ushort LeatherworkerLevel { get; set; }
		public ushort WeaverLevel { get; set; }
		public ushort AlchemistLevel { get; set; }
		public ushort CulinarianLevel { get; set; }
		public ushort MinerLevel { get; set; }
		public ushort BotanistLevel { get; set; }
		public ushort FisherLevel { get; set; }
		public ushort ArcanistLevel { get; set; }
		public ushort RogueLevel { get; set; }
		public ushort MachinistLevel { get; set; }
		public ushort DarkKnightLevel { get; set; }
		public ushort AstrologianLevel { get; set; }
		public ushort SamuraiLevel { get; set; }
		public ushort RedMageLevel { get; set; }
		public uint Strength { get; set; }
		public uint Dexterity { get; set; }
		public uint Vitality { get; set; }
		public uint Intelligence { get; set; }
		public uint Mind { get; set; }
		public uint Piety { get; set; }
		public uint FireResistance { get; set; }
		public uint IceResistance { get; set; }
		public uint WindResistance { get; set; }
		public uint EarthResistance { get; set; }
		public uint LightningResistance { get; set; }
		public uint WaterResistance { get; set; }
		public uint Accuracy { get; set; }
		public uint CriticalHitRate { get; set; }
		public uint Determination { get; set; }
		public uint Defense { get; set; }
		public uint Parry { get; set; }
		public uint MagicDefense { get; set; }
		public uint AttackPower { get; set; }
		public uint SkillSpeed { get; set; }
		public uint Gathering { get; set; }
		public uint Perception { get; set; }
		public uint SlowResistance { get; set; }
		public uint SilenceResistance { get; set; }
		public uint BlindResistance { get; set; }
		public uint PoisonResistance { get; set; }
		public uint StunResistance { get; set; }
		public uint SleepResistance { get; set; }
		public uint BindResistance { get; set; }
		public uint HeavyResistance { get; set; }
	}
}
