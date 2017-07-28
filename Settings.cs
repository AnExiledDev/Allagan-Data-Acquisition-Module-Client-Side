using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ff14bot.Helpers;

namespace ADAM
{
	internal class Settings : JsonSettings
	{
		public static string SettingsFilePath = Path.Combine(SettingsPath, @"ADAM\Settings.json");

		public Settings(string path) : base(SettingsFilePath) { }
		public static Settings Instance = new Settings(SettingsFilePath);

		public string CoreAuthKey { get; set; }

		internal string SetCoreAuthKey(string key)
		{
			CoreAuthKey = key.Trim();
			Save();

			string result = Server.ValidateKey(key);

			if (result == "true")
			{
				return "Key Validated!";
			}
			
			if (result == "false")
			{
				return "Invalid Key.";
			}
			
			Helper.Log("Key Validation Failed: " + result);
			return "Server unresponsive.";
		}
	}
}
