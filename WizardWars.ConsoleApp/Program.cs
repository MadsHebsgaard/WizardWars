﻿using Newtonsoft.Json;
using WizardWars.Lib;

namespace WizardWars.ConsoleApp;

public static class Program
{
	private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
	{
		TypeNameHandling = TypeNameHandling.Auto
	};

	public static void Main()
	{
		var spellsFromJson = ReadFromJson<IEnumerable<Spell>>("spells.json");

		var firstPlayerName = GetInputString("Enter first player name: ");
		var secondPlayerName = GetInputString("Enter second player name: ");

		var wizard1 = new Wizard(firstPlayerName);
		var wizard2 = new Wizard(secondPlayerName);

		while (true)
		{
			var p1 = new SpellTarget(wizard1, spellsFromJson.First(), wizard2);
			var p2 = new SpellTarget(wizard2, spellsFromJson.First(), wizard1);
			var turn = new Turn(p1, p2);

			// who won?
		}
	}

	private static T ReadFromJson<T>(string filename)
	{
		return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename), _jsonSerializerSettings);
	}

	private static string GetInputString(string prompt)
	{
		string? input;
		do
		{
			Console.Write(prompt);
			input = Console.ReadLine();
		} while (string.IsNullOrWhiteSpace(input));

		return input;
	}
	
	// private static void GenerateSampleSpellJson()
	// {
	// 	var spells = new List<Spell>
	// 	{
	// 		new Spell()
	// 		{
	// 			Name = "Lifedrain",
	// 			Effects = new List<Effect>
	// 			{
	// 				new HealEffect()
	// 				{
	// 					Phase = SpellPhase.Four,
	// 					HealAmount = 15
	// 				},
	// 				new DamageEffect()
	// 				{
	// 					Phase = SpellPhase.Five,
	// 					Damage = 10
	// 				}
	// 			}
	// 		}
	// 	};
	//
	// 	var json = JsonConvert.SerializeObject(spells, Formatting.Indented, new JsonSerializerSettings
	// 		{
	// 			TypeNameHandling = TypeNameHandling.Auto
	// 		}
	// 	);
	//
	// 	Console.WriteLine(json);
	// }
}