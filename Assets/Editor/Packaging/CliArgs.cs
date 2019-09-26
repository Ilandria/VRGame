using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CCB.MechGame.Editor.Packaging
{
	public struct CliArgs
	{
		private readonly Dictionary<string, string> arguments;

		public CliArgs(string[] args)
		{
			arguments = new Dictionary<string, string>();

			Regex argRegex = new Regex(@"^-[A-Za-z]");

			Console.WriteLine("Parsing CLI arguments.");
			for (int i = 0; i < args.Length; i++)
			{
				if (argRegex.Match(args[i]).Success)
				{
					string argument = args[i].Trim('-').ToLower();
					string value = "";

					if (i + 1 < args.Length && !argRegex.Match(args[i + 1]).Success)
					{
						value = args[i + 1];
						i++;
					}

					Console.WriteLine($"Parsed argument: \"{argument}\" : \"{value}\".");
					arguments.Add(argument, value);
				}
			}
		}

		public string GetValue(string argument)
		{
			return arguments[argument.ToLower()];
		}

		public T GetValueAs<T>(string argument)
		{
			return (T)Convert.ChangeType(arguments[argument.ToLower()], typeof(T));
		}

		public string this[string argument]
		{
			get { return GetValue(argument); }
		}
	}
}