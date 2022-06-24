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
		IUserInterface userInterface = new SpectreConsoleUserInterface();
		
		//Spell setup
		var spellsFromJson = ReadFromJson<IEnumerable<Spell>>("spells.json").ToList();

		//Wizard setup
		var firstPlayerName = userInterface.GetPromptedText("Enter first player name: ");
		var secondPlayerName = userInterface.GetPromptedText("Enter second player name: ");
		var wizard1 = new Wizard(firstPlayerName);
		var wizard2 = new Wizard(secondPlayerName);

		//var p2Spells = userInterface.UserPicksSpell(wizard2, spellsFromJson.Where(x => x.Name == "Meditate").ToList());

		//Duel setup
		Console.WriteLine("\n ------------------------------  ");
		int maxTurns = 100;
		int turnNumber = 0;

		while (wizard1.Health >= 0 && wizard2.Health >= 0 && turnNumber < maxTurns)
		{
			turnNumber++;

			//Known spells
			var p1SpellList = spellsFromJson.Where(x => x.IQRequired <= wizard1.IQ).ToList();
			var p2SpellList = spellsFromJson.Where(x => x.IQRequired <= wizard2.IQ).ToList();

			Console.WriteLine("Turn: " + turnNumber);
			Console.WriteLine(" ------------------------------  ");
			userInterface.DisplayStats(wizard1, wizard2);

			var p1Spell = userInterface.UserPicksSpell(wizard1, p1SpellList.Where(x => x.ManaCost <= wizard1.Mana).ToList());
			var p1Target = userInterface.UserPicksTarget() == Target.Self ? wizard1 : wizard2;
			var p1 = new SpellTarget(wizard1, p1Spell, p1Target);

			var p2Spell = userInterface.UserPicksSpell(wizard2, p2SpellList.Where(x => x.ManaCost <= wizard2.Mana).ToList());
			var p2Target = userInterface.UserPicksTarget() == Target.Self ? wizard2 : wizard1;
			var p2 = new SpellTarget(wizard2, p2Spell, p2Target);


			Console.WriteLine();
			var turn = new Turn(p1, p2);
			turn.Execute();

			wizard1.Mana -= p1Spell.ManaCost;
			wizard2.Mana -= p2Spell.ManaCost;


			//Show events
			Console.WriteLine(wizard1.Name + " used " + p1Spell.Name + " at " + p1Target.Name);
			Console.WriteLine(wizard2.Name + " used " + p2Spell.Name + " at " + p2Target.Name + "\n");

			//userInterface.DisplayStats(wizard1, wizard2);
			
			Console.WriteLine(" ------------------------------ ");
			
			// TODO: show regen effects

			wizard1.Health += wizard1.HealthRegen;
			wizard2.Health += wizard1.HealthRegen;
			wizard1.Mana += wizard1.ManaRegen;
			wizard2.Mana += wizard1.ManaRegen;
			wizard1.IQ += wizard1.IQRegen;
			wizard2.IQ += wizard2.IQRegen;
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
		return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename), _jsonSerializerSettings); //"Possible null reference return"
	}
}
