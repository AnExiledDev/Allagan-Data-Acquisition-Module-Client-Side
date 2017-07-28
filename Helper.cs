using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ff14bot.Helpers;
using System.IO;

namespace ADAM
{
	internal class Helper
	{
		internal static readonly Object _logFileLock = new Object();
		internal static readonly string LogsFolderPath = Path.Combine(Environment.CurrentDirectory, @"Plugins\ADAM\Logs\");

		internal static void Log(string message)
		{
			Logging.Write(Colors.SteelBlue, "[ADAM] " + message);
		}

		internal static string EncodeNonAsciiCharacters(string value)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in value)
			{
				if (c > 127)
				{
					// This character is too big for ASCII
					string encodedValue = "\\u" + ((int)c).ToString("x4");
					sb.Append(encodedValue);
				}
				else
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		internal static void LogToFile(string message)
		{
			FileInfo fileInfo = new FileInfo(LogsFolderPath);

			if (!fileInfo.Exists)
			{
				try
				{
					Directory.CreateDirectory(fileInfo.Directory.FullName);
				}
				catch (Exception e)
				{
					Helper.Log("Failed creating Logs Directory. You can probably ignore this error.");
				}
			}

			try
			{
				lock (_logFileLock)
				{
					DateTime currentUtcTime = DateTime.Now;

					TextWriter tw = new StreamWriter(LogsFolderPath + currentUtcTime.ToShortDateString().Replace("/", "-") + ".log", true);
					tw.WriteLine("[" + currentUtcTime + "]" + message);
					tw.Close();
				}
			}
			catch (Exception e)
			{

			}
		}
	}
}
