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
		var firstPlayerName = userInterface.GetPromptedText("Enter first player name : ");
		var secondPlayerName = userInterface.GetPromptedText("Enter second player name: ");
		var wizard1 = new Wizard(firstPlayerName);
		var wizard2 = new Wizard(secondPlayerName);

		//Test Enviroment
		TestEnviroment(wizard1, wizard2);

		//Duel setup
		Console.WriteLine();
		int turnNumber = 0, maxTurns = 100;

		while (wizard1.Health >= 0 && wizard2.Health >= 0 && turnNumber < maxTurns)
		{	turnNumber++;

			//Known spells
			var p1SpellList = spellsFromJson.Where(x => x.LVLRequired <= wizard1.LVL).ToList();
			var p2SpellList = spellsFromJson.Where(x => x.LVLRequired <= wizard2.LVL).ToList();

			userInterface.DisplayTurnNumber(turnNumber);

			//userInterface.DisplayStats(wizard1, wizard2);		//Matrix Form
			userInterface.DisplayStatsGraph(wizard1, wizard2);	//Graph Form


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
			UpdateStats(wizard1, wizard2, p1Spell.ManaCost, p2Spell.ManaCost);
		}
		userInterface.DisplayWinText(wizard1, wizard2, turnNumber, maxTurns);
	}

    private static void UpdateStats(Wizard wizard1, Wizard wizard2, int ManaCost1, int ManaCost2)
    {
		wizard1.Health = wizard1.Health + wizard1.HealthRegen > 100 ? 100 : wizard1.Health + wizard1.HealthRegen;
		wizard2.Health = wizard2.Health + wizard2.HealthRegen > 100 ? 100 : wizard2.Health + wizard2.HealthRegen;
		wizard1.Mana = wizard1.Mana + wizard1.ManaRegen - ManaCost1 > 100 ? 100 : wizard1.Mana + wizard1.ManaRegen - ManaCost1;
		wizard2.Mana = wizard2.Mana + wizard2.ManaRegen - ManaCost2 > 100 ? 100 : wizard2.Mana + wizard2.ManaRegen - ManaCost2;
		wizard1.Mana = wizard1.Mana < 0 ? 0 : wizard1.Mana;
		wizard2.Mana = wizard2.Mana < 0 ? 0 : wizard2.Mana;
		wizard1.LVLRegen = wizard1.LVL < 10 ? 1 / Math.Floor(wizard1.LVL) : 0;
		wizard2.LVLRegen = wizard2.LVL < 10 ? 1 / Math.Floor(wizard2.LVL) : 0;
		wizard1.LVL = wizard1.LVL + wizard1.LVLRegen > 10 ? 10 : Math.Round(wizard1.LVL + wizard1.LVLRegen, 2);
		wizard2.LVL = wizard2.LVL + wizard2.LVLRegen > 10 ? 10 : Math.Round(wizard2.LVL + wizard2.LVLRegen, 2);
	}

    private static T ReadFromJson<T>(string filename)
	{
		return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename), _jsonSerializerSettings); //"Possible null reference return"
	}


	private static void TestEnviroment(Wizard wizard1, Wizard wizard2)
	{
		wizard1.LVL = wizard1.Name == "test1" ? 10 : 1;
		wizard2.LVL = wizard2.Name == "test2" ? 10 : 1;
	}
}