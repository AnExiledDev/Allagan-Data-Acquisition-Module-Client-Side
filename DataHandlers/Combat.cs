using ADAM.DataClasses;
using ff14bot.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ff14bot;
using Newtonsoft.Json;

namespace ADAM.DataHandlers
{
	internal class Combat
	{
		internal static ChatLogEntry _lastCombatMessage;

		internal static void HandleCombatMessages(List<ChatLogEntry> chatLogEntries)
		{
			CombatMessage combatMessage = null;

			if (
				_lastCombatMessage != null &&
				(
					Regex.Match(_lastCombatMessage.Contents, "You (?:cast|use) (.*).").Success ||
					Regex.Match(_lastCombatMessage.Contents, "The (.*) (?:casts|uses) (.*).").Success ||
					Regex.Match(_lastCombatMessage.Contents, @"The (.*) takes (?:(.*)\((?:.*)\)|(.*)) damage.").Success ||
					Regex.Match(_lastCombatMessage.Contents, "The attack misses the (.*).").Success
				)
			)
			{
				chatLogEntries.Insert(0, _lastCombatMessage);
			}

			bool isTwoPart = false;
			foreach (ChatLogEntry entry in chatLogEntries)
			{
				string message = entry.Contents;

				Match yourSpellCast = Regex.Match(message, "You (?:cast|use) (.*)."); // You casting spell - 1st Part
				Match theirSpellCast = Regex.Match(message, "The (.*) (?:casts|uses) (.*)."); // Someone else casting spell - 1st Part
				Match yourAutoAttack = Regex.Match(message, "You hit the (.*) for (.*) damage."); // You're auto attack
				Match theirAutoAttack = Regex.Match(message, @"The (.*) hits you for (.*)\((?:.*)\) damage."); // Auto attack not involving you
				Match otherAutoAttack = Regex.Match(message, "The (.*) hits the (.*) for (.*) damage."); // Auto attack not involving you
				Match damageToOther = Regex.Match(message, @"The (.*) takes (?:(.*)\((?:.*)\)|(.*)) damage."); // Damage done to other - 2nd Part
				Match attackMissed = Regex.Match(message, "The attack misses the (.*)."); // Attack missed - 2nd Part
				Match autoAttackMissed = Regex.Match(message, "The (.*) misses (.*)."); // Auto attack missed
				Match criticalAttack = Regex.Match(message, "Critical!"); // Critical attack - Part of 2nd Part

				// Check if combat message
				if (
					!yourSpellCast.Success &&
					!theirSpellCast.Success &&
					!yourAutoAttack.Success &&
					!theirAutoAttack.Success &&
					!damageToOther.Success &&
					!attackMissed.Success &&
					!autoAttackMissed.Success
				)
				{
					continue;
				}

				// If is first part, but expecting second part, continue
				if ((yourSpellCast.Success || theirSpellCast.Success) && isTwoPart == true)
				{
					//Helper.Log("DEBUG: Received first part, but expected second part! " + message);
					isTwoPart = false;
					continue;
				}

				// If is second part, but not expecting second part, continue
				if (damageToOther.Success && isTwoPart == false)
				{
					//Helper.Log("DEBUG: Received second part, but expected first part!" + message);
					continue;
				}

				// Is Two Part or One Part?
				if (
					yourSpellCast.Success ||
					theirSpellCast.Success
				)
				{
					isTwoPart = true;
				}


				if (combatMessage == null)
				{
					combatMessage = new CombatMessage();
					combatMessage.CoreAuthKey = Settings.Instance.CoreAuthKey;
					combatMessage.PlayerName = Core.Player.Name;
					combatMessage.TimeStamp = entry.TimeStamp.ToString("yyyy'/'MM'/'dd HH':'mm':'ss");
					combatMessage.IsAutoAttack = false;
					combatMessage.IsMiss = false;
					combatMessage.IsCriticalAttack = false;
				}

				// Is combat message, determine if is: 1st Part, 2nd Part, or Auto Attack
				if (yourSpellCast.Success)
				{
					// Is first part
					combatMessage.Attacker = Core.Player.Name;
					//combatMessage.Attackee = ;
					combatMessage.Class = Core.Player.CurrentJob.ToString();
					combatMessage.Spell = yourSpellCast.Groups[1].Value;
					//combatMessage.Damage = ;
				}

				if (theirSpellCast.Success)
				{
					// Is first part
					combatMessage.Attacker = theirSpellCast.Groups[1].Value;
					//combatMessage.Attackee = ;
					combatMessage.Class = "";
					combatMessage.Spell = theirSpellCast.Groups[2].Value;
					//combatMessage.Damage = ;
				}

				if (damageToOther.Success)
				{
					// Is second part
					//combatMessage.Attacker = ;
					combatMessage.Attackee = damageToOther.Groups[1].Value;
					//combatMessage.Class = ;
					//combatMessage.Spell = ;

					int damage = 0;
					if (damageToOther.Groups[2].Value != null)
					{
						damage = Convert.ToInt32(damageToOther.Groups[2].Value);
					}
					else
					{
						damage = Convert.ToInt32(damageToOther.Groups[3].Value);
					}

					if (combatMessage.Damage > 0)
					{
						combatMessage.Damage += damage;
					}
					else
					{
						combatMessage.Damage = damage;
					}
				}

				if (yourAutoAttack.Success)
				{
					// Is auto attack
					combatMessage.Attacker = Core.Player.Name;
					combatMessage.Attackee = yourAutoAttack.Groups[1].Value;
					combatMessage.Class = Core.Player.CurrentJob.ToString();
					combatMessage.Spell = "Auto Attack";
					combatMessage.IsAutoAttack = true;
					combatMessage.Damage = Convert.ToInt32(yourAutoAttack.Groups[2].Value);
				}

				if (theirAutoAttack.Success)
				{
					// Is auto attack
					combatMessage.Attacker = theirAutoAttack.Groups[1].Value;
					combatMessage.Attackee = Core.Player.Name;
					combatMessage.Class = "";
					combatMessage.Spell = "Auto Attack";
					combatMessage.IsAutoAttack = true;
					combatMessage.Damage = Convert.ToInt32(theirAutoAttack.Groups[2].Value);
				}

				if (otherAutoAttack.Success)
				{
					// Is auto attack
					combatMessage.Attacker = otherAutoAttack.Groups[1].Value;
					combatMessage.Attackee = otherAutoAttack.Groups[2].Value;
					combatMessage.Class = "";
					combatMessage.Spell = "Auto Attack";
					combatMessage.IsAutoAttack = true;
					combatMessage.Damage = Convert.ToInt32(otherAutoAttack.Groups[3].Value);
				}

				if (criticalAttack.Success)
				{
					// Is critical attack
					combatMessage.IsCriticalAttack = true;
				}

				if (autoAttackMissed.Success)
				{
					// Is missed auto attack
					combatMessage.IsMiss = true;
				}

				if (attackMissed.Success)
				{
					// Is missed attack
					combatMessage.Attackee = attackMissed.Groups[1].Value;
					combatMessage.IsMiss = true;
					combatMessage.Damage = 0;
				}

				if (combatMessage.Attacker != null && combatMessage.Attackee != null)
				{
					Server.QueueMessage(JsonConvert.SerializeObject(combatMessage));
					combatMessage = null;
				}

				_lastCombatMessage = entry;
			}
		}
	}
}
