using Newtonsoft.Json;
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
		//Spell setup
		var spellsFromJson = ReadFromJson<IEnumerable<Spell>>("spells.json");

		//Wizard setup
		var firstPlayerName = GetInputString("Enter first player name: ");
		var secondPlayerName = GetInputString("Enter second player name: ");
		var wizard1 = new Wizard(firstPlayerName);
		var wizard2 = new Wizard(secondPlayerName);

		//Duel setup
		Console.WriteLine("\n ------------------------------  ");
		int maxTurns = 100;
		int turnNumber = 0;
		while (wizard1.Health >= 0 && wizard2.Health >= 0 && turnNumber < maxTurns)
		{
			turnNumber++;

			Console.WriteLine("Turn: " + turnNumber);
			Console.WriteLine(" ------------------------------  ");
			Stats(wizard1, wizard2);

			var firstPlayerSpellString = GetInputString("\nEnter your spell: ");
			var secondPlayerSpellString = GetInputString("Enter your spell: ");
			Console.WriteLine();

			//Need to understand this!
			var p1 = new SpellTarget(wizard1, spellsFromJson.First(), wizard2);
			var p2 = new SpellTarget(wizard2, spellsFromJson.First(), wizard1);
			var turn = new Turn(p1, p2);

			//var turn = new Turn(SpellTarget(wizard1, spellsFromJson.First(), wizard2), SpellTarget(wizard2, spellsFromJson.First(), wizard1));

			//Show events and status.
			Console.WriteLine(wizard1.Name + " used " + spellsFromJson.First().Name + " at " + wizard2.Name);
			Console.WriteLine(wizard2.Name + " used " + spellsFromJson.First().Name + " at " + wizard1.Name + "\n");

			Stats(wizard1, wizard2);


			Console.WriteLine(" ------------------------------ ");

			wizard1.Health += wizard1.HealthRegen;
			wizard2.Health += wizard1.HealthRegen;
			wizard1.Mana += wizard1.ManaRegen;
			wizard2.Mana += wizard1.ManaRegen;

		}

		//Text after duel is over:
		if (turnNumber != maxTurns)
		{
			if (wizard1.Health > 0)
			{
				Console.WriteLine(wizard1.Name + " killed " + wizard2.Name + " and won the Duel!");
			}
			else if (wizard2.Health > 0)
			{
				Console.WriteLine(wizard2.Name + " murdered " + wizard1.Name + " and won the Duel!");
			}
			else
			{
				Console.WriteLine(wizard1.Name + " and " + wizard2.Name + " killed each other and the Duel is lost for both but their honors remain intact!");
			}
		}
		else	
		{ 
			Console.WriteLine("Dual over! Ran out of max turns of " + maxTurns); 
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


	public static void Stats(Wizard wizard1, Wizard wizard2)
	{
		int s;
		s = Math.Max(wizard1.Name.Length, wizard2.Name.Length);
		
		Console.WriteLine(wizard1.Name.PadRight(s) + ": Hp = " + wizard1.Health + "  Mana = " + wizard1.Mana);
		Console.WriteLine(wizard2.Name.PadRight(s) + ": Hp = " + wizard2.Health + "  Mana = " + wizard2.Mana);
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
