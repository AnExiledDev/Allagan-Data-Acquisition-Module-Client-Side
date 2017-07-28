using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADAM.DataClasses;
using ff14bot;
using ff14bot.Enums;
using ff14bot.Objects;
using ff14bot.RemoteWindows;
using Newtonsoft.Json;

namespace ADAM.DataHandlers
{
	internal class Character
	{
		internal static void GetPlayerData()
		{
			if (Server.IsSocketConnected() == false || Core.Player == null)
			{
				return;
			}

			LocalPlayer player = Core.Player;
			Dictionary<ClassJobType, ushort> playerLevels = player.Levels;
			Stats playerStats = player.Stats;

			PlayerDataMessage playerDataMessage = new PlayerDataMessage();
			playerDataMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
			playerDataMessage.PlayerName = player.Name;
			playerDataMessage.CurrentJob = player.CurrentJob.ToString();
			playerDataMessage.Location = player.Location.ToString();
			playerDataMessage.IdLocation = player.IdLocation;
			playerDataMessage.FateId = player.FateId;
			playerDataMessage.CurrentHealth = player.CurrentHealth;
			playerDataMessage.MaxHealth = player.MaxHealth;
			playerDataMessage.CurrentMana = player.CurrentMana;
			playerDataMessage.MaxMana = player.MaxMana;
			playerDataMessage.CurrentTP = player.CurrentTP;
			playerDataMessage.MaxTP = player.MaxTP;
			playerDataMessage.CurrentExperience = Experience.CurrentExperience;
			playerDataMessage.CurrentRestedExperience = Experience.CurrentRestedExperience;
			playerDataMessage.ExperienceRequired = Experience.ExperienceRequired;
			playerDataMessage.GladiatorLevel = playerLevels[ClassJobType.Gladiator];
			playerDataMessage.PugilistLevel = playerLevels[ClassJobType.Pugilist];
			playerDataMessage.MarauderLevel = playerLevels[ClassJobType.Marauder];
			playerDataMessage.LancerLevel = playerLevels[ClassJobType.Lancer];
			playerDataMessage.ArcherLevel = playerLevels[ClassJobType.Archer];
			playerDataMessage.ConjurerLevel = playerLevels[ClassJobType.Conjurer];
			playerDataMessage.ThaumaturgeLevel = playerLevels[ClassJobType.Thaumaturge];
			playerDataMessage.CarpenterLevel = playerLevels[ClassJobType.Carpenter];
			playerDataMessage.BlacksmithLevel = playerLevels[ClassJobType.Blacksmith];
			playerDataMessage.ArmorerLevel = playerLevels[ClassJobType.Armorer];
			playerDataMessage.GoldsmithLevel = playerLevels[ClassJobType.Goldsmith];
			playerDataMessage.LeatherworkerLevel = playerLevels[ClassJobType.Leatherworker];
			playerDataMessage.WeaverLevel = playerLevels[ClassJobType.Weaver];
			playerDataMessage.AlchemistLevel = playerLevels[ClassJobType.Alchemist];
			playerDataMessage.CulinarianLevel = playerLevels[ClassJobType.Culinarian];
			playerDataMessage.MinerLevel = playerLevels[ClassJobType.Miner];
			playerDataMessage.BotanistLevel = playerLevels[ClassJobType.Botanist];
			playerDataMessage.FisherLevel = playerLevels[ClassJobType.Fisher];
			playerDataMessage.ArcanistLevel = playerLevels[ClassJobType.Arcanist];
			playerDataMessage.RogueLevel = playerLevels[ClassJobType.Rogue];
			playerDataMessage.MachinistLevel = playerLevels[ClassJobType.Machinist];
			playerDataMessage.DarkKnightLevel = playerLevels[ClassJobType.DarkKnight];
			playerDataMessage.AstrologianLevel = playerLevels[ClassJobType.Astrologian];
			playerDataMessage.SamuraiLevel = playerLevels[ClassJobType.Samurai];
			playerDataMessage.RedMageLevel = playerLevels[ClassJobType.RedMage];
			playerDataMessage.Strength = playerStats.Strength;
			playerDataMessage.Dexterity = playerStats.Dexterity;
			playerDataMessage.Vitality = playerStats.Vitality;
			playerDataMessage.Intelligence = playerStats.Intelligence;
			playerDataMessage.Mind = playerStats.Mind;
			playerDataMessage.Piety = playerStats.Piety;
			playerDataMessage.FireResistance = 0; //playerStats.FireResistance
			playerDataMessage.IceResistance = 0; //playerStats.IceResistance
			playerDataMessage.WindResistance = 0; //playerStats.WindResistance
			playerDataMessage.EarthResistance = 0; //playerStats.EarthResistance
			playerDataMessage.LightningResistance = 0; //playerStats.LightningResistance
			playerDataMessage.WaterResistance = 0; //playerStats.WaterResistance
			playerDataMessage.Accuracy = 0; //playerStats.Accuracy
			playerDataMessage.CriticalHitRate = 0; //playerStats.CriticalHitRate
			playerDataMessage.Determination = 0; //playerStats.Determination
			playerDataMessage.Defense = playerStats.Defense;
			playerDataMessage.Parry = 0; //playerStats.Parry
			playerDataMessage.MagicDefense = playerStats.MagicDefense;
			playerDataMessage.AttackPower = playerStats.AttackPower;
			playerDataMessage.SkillSpeed = playerStats.SkillSpeed;
			playerDataMessage.Gathering = playerStats.Gathering;
			playerDataMessage.Perception = playerStats.Perception;
			playerDataMessage.SlowResistance = 0; //playerStats.SlowResistance
			playerDataMessage.SilenceResistance = 0; //playerStats.SilenceResistance
			playerDataMessage.BlindResistance = 0; //playerStats.BlindResistance
			playerDataMessage.PoisonResistance = 0; //playerStats.PoisonResistance
			playerDataMessage.StunResistance = 0; //playerStats.StunResistance
			playerDataMessage.SleepResistance = 0; //playerStats.SleepResistance
			playerDataMessage.BindResistance = 0; //playerStats.BindResistance
			playerDataMessage.HeavyResistance = 0; //playerStats.HeavyResistance

			Server.QueueMessage(JsonConvert.SerializeObject(playerDataMessage));
		}

		internal static void GetPlayerDataEvent(object sender, EventArgs e)
		{
			GetPlayerData();
		}
	}
}
