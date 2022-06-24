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
		//UI and Spell setup
		IUserInterface userInterface = new SpectreConsoleUserInterface();
		var spellsFromJson = ReadFromJson<IEnumerable<Spell>>("spells.json").ToList();

		//Wizard setup
		var firstPlayerName = userInterface.GetPromptedText("Enter first player name: ");
		var secondPlayerName = userInterface.GetPromptedText("Enter second player name: ");
		var wizard1 = new Wizard(firstPlayerName);
		var wizard2 = new Wizard(secondPlayerName);

		//Duel setup
		Console.WriteLine();
		int turnNumber = 0, maxTurns = 100;

		while (wizard1.Health >= 0 && wizard2.Health >= 0 && turnNumber < maxTurns)
		{	turnNumber++;

			//Known spells
			var p1SpellList = spellsFromJson.Where(x => x.IQRequired <= wizard1.IQ).ToList();
			var p2SpellList = spellsFromJson.Where(x => x.IQRequired <= wizard2.IQ).ToList();

			//Stats before casting spells
			Console.WriteLine($"\n -------------------------------\n  	    Turn: {turnNumber}\n -------------------------------  ");
			userInterface.DisplayStats(wizard1, wizard2);

			//player 1 and 2 moves.
			var p1Spell = userInterface.UserPicksSpell(wizard1, p1SpellList.Where(x => x.ManaCost <= wizard1.Mana).ToList());
			var p1Target = userInterface.UserPicksTarget() == Target.Self ? wizard1 : wizard2;
			var p1 = new SpellTarget(wizard1, p1Spell, p1Target);
			var p2Spell = userInterface.UserPicksSpell(wizard2, p2SpellList.Where(x => x.ManaCost <= wizard2.Mana).ToList());
			var p2Target = userInterface.UserPicksTarget() == Target.Self ? wizard2 : wizard1;
			var p2 = new SpellTarget(wizard2, p2Spell, p2Target);

			//Create, execute and show turn
			var turn = new Turn(p1, p2);
			turn.Execute();

			userInterface.DisplayEventLog(turn.EventLog);

			// TODO: add these to seperate "Regen" function (wizard1, wizard2, spell1, spell2)
			wizard1.Health += wizard1.HealthRegen;
			wizard2.Health += wizard1.HealthRegen;
			wizard1.Mana += wizard1.ManaRegen- p1Spell.ManaCost;
			wizard2.Mana += wizard1.ManaRegen- p2Spell.ManaCost;
			wizard1.IQ += wizard1.IQRegen;
			wizard2.IQ += wizard2.IQRegen;
		}
		userInterface.DisplayWinText(wizard1, wizard2, turnNumber, maxTurns);
	}
	private static T ReadFromJson<T>(string filename)
	{
		return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename), _jsonSerializerSettings); //"Possible null reference return"
	}
}