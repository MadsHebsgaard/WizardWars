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
		IUserInterface userInterface = new ConsoleUserInterface();
		
		//Spell setup
		var spellsFromJson = ReadFromJson<IEnumerable<Spell>>("spells.json").ToList();

		//Wizard setup
		var firstPlayerName = userInterface.GetPromptedText("Enter first player name: ");
		var secondPlayerName = userInterface.GetPromptedText("Enter second player name: ");
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
			userInterface.DisplayStats(wizard1, wizard2);

			var player1Spell = userInterface.UserPicksSpell(wizard1, spellsFromJson);
			var player1Target = userInterface.UserPicksTarget();
			var p1Target = player1Target == Target.Self ? wizard1 : wizard2;

			var player2Spell = userInterface.UserPicksSpell(wizard2, spellsFromJson);
			var player2Target = userInterface.UserPicksTarget();
			var p2Target = player2Target == Target.Self ? wizard2 : wizard1;

			Console.WriteLine();

			//Need to understand this!
			var p1 = new SpellTarget(wizard1, player1Spell, p1Target);
			var p2 = new SpellTarget(wizard2, player2Spell, p2Target);
			var turn = new Turn(p1, p2);

			turn.Execute();

			//Show events and status.
			Console.WriteLine(wizard1.Name + " used " + spellsFromJson.First().Name + " at " + wizard2.Name);
			Console.WriteLine(wizard2.Name + " used " + spellsFromJson.First().Name + " at " + wizard1.Name + "\n");

			userInterface.DisplayStats(wizard1, wizard2);
			
			Console.WriteLine(" ------------------------------ ");
			
			// TODO: show regen effects

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
