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
		int numberOfSpells = 12;
		int wz1 = 1;
		int wzMax = 2;

		//UI and Spell setup
		IUserInterface userInterface = new SpectreConsoleUserInterface();
		var spellsFromJson = ReadFromJson<IEnumerable<Spell>>("spells.json").ToList();
		userInterface.DisplayWizardWars();


		Wizard wizard1 = GetWizard("First Player ", userInterface);
		Wizard wizard2 = GetWizard("Second Player", userInterface);

		while (true)
        {
			ResetWizard(wizard1);
			ResetWizard(wizard2);

			var Spellbook1 = userInterface.GetSpells(spellsFromJson, numberOfSpells, wizard1.Name);
			var Spellbook2 = userInterface.GetSpells(spellsFromJson, numberOfSpells, wizard2.Name);

			//Duel setup
			Console.WriteLine();
			int turnNumber = 1, maxTurns = 100;

			while (wizard1.Health > 0 && wizard2.Health > 0 && turnNumber < maxTurns)
			{	

				//Known spells
				var p1SpellList = Spellbook1.Where(x => x.LVLRequired <= wizard1.LVL).ToList();
				var p2SpellList = Spellbook2.Where(x => x.LVLRequired <= wizard2.LVL).ToList();

				userInterface.DisplayTurnNumber(turnNumber);

				if (wz1 == 1) { userInterface.DisplayStatsGraph(wizard1, wizard2); }
				else if (wz1 == 2) { userInterface.DisplayStatsGraph(wizard2, wizard1); }

				//userInterface.DisplayStats(wizard1, wizard2);		//TODO: Remove this
				Console.WriteLine();

				//player 1 and 2 moves.
				var p1Spell = userInterface.UserPicksSpell(wizard1, p1SpellList.Where(x => x.ManaCost < wizard1.Mana && x.HealthCost < wizard1.Health).ToList());
				var p1Target = GetTarget(p1Spell, userInterface, wizard1, wizard2);
				var p1 = new SpellTarget(wizard1, p1Spell, p1Target);
				//Console.Beep();
				var p2Spell = userInterface.UserPicksSpell(wizard2, p2SpellList.Where(x => x.ManaCost < wizard2.Mana && x.HealthCost < wizard2.Health).ToList());
				var p2Target = GetTarget(p2Spell, userInterface, wizard2, wizard1);
				var p2 = new SpellTarget(wizard2, p2Spell, p2Target);

				//Create, execute and show turn
				var turn = new Turn(p1, p2);
				turn.Execute();
				userInterface.DisplayEventLog(turn.EventLog);
				turnNumber++;

				if (wizard1.Health > 0 && wizard2.Health > 0 && turnNumber < maxTurns)
                {
					UpdateStats(wizard1, wizard2, p1Spell.ManaCost, p2Spell.ManaCost, p1Spell.HealthCost, p2Spell.HealthCost);
					Wizard wizard0 = wizard1;
					wizard1 = wizard2;
					wizard2 = wizard0;
					wizard0.Delete(); //Does it have to be deleted?
					wz1 = wz1 < wzMax ? wz1 + 1 : 1;
				}
			}
			userInterface.DisplayWinText(wizard1, wizard2, turnNumber, maxTurns, wz1);
		}
	}

	private static void UpdateStats(Wizard wizard1, Wizard wizard2, int ManaCost1, int ManaCost2, int HealthCost1, int HealthCost2)
    {
		double lvlBefore1 = Math.Floor(wizard1.LVL);
		double lvlBefore2 = Math.Floor(wizard2.LVL);

		wizard1.Resistance = 0;
		wizard2.Resistance = 0;

		wizard1.Health += wizard1.HealthRegen;
		wizard2.Health += wizard2.HealthRegen;
		wizard1.Mana += wizard1.ManaRegen;
		wizard2.Mana += wizard2.ManaRegen;

		wizard1.Mana = wizard1.Mana < wizard1.MaxMana ? wizard1.Mana : wizard1.MaxMana;
		wizard2.Mana = wizard2.Mana < wizard2.MaxMana ? wizard2.Mana : wizard2.MaxMana;
		wizard1.Health = wizard1.Health < wizard1.MaxHealth ? wizard1.Health : wizard1.MaxHealth;
		wizard2.Health = wizard2.Health < wizard2.MaxHealth ? wizard2.Health : wizard2.MaxHealth;

		wizard1.Mana = wizard1.Mana < 0 ? 0 : wizard1.Mana;
		wizard2.Mana = wizard2.Mana < 0 ? 0 : wizard2.Mana;

		//LVL up!
		if (wizard1.LVL != Wizard.MaxLVL)
        {
			wizard1.LVLRegen = 1 / Math.Floor(wizard1.LVL + 1) + 0.1;
			wizard1.LVL = wizard1.LVL + wizard1.LVLRegen > Wizard.MaxLVL ? Wizard.MaxLVL : Math.Round(wizard1.LVL + wizard1.LVLRegen, 2);
			wizard1.LVL = wizard1.LVL % 1 > 0.95 ? Math.Ceiling(wizard1.LVL) : wizard1.LVL;
			int LVLup1 = Convert.ToInt32(Math.Floor(wizard1.LVL - lvlBefore1));
			wizard1.Health += LVLup1 * Wizard.LVLHeal;
		}
		if (wizard2.LVL != Wizard.MaxLVL)
		{
			wizard2.LVLRegen = 1 / Math.Floor(wizard2.LVL + 1) + 0.1;
			wizard2.LVL = wizard2.LVL + wizard2.LVLRegen > Wizard.MaxLVL ? Wizard.MaxLVL : Math.Round(wizard2.LVL + wizard2.LVLRegen, 2);
			wizard2.LVL = wizard2.LVL % 1 > 0.95 ? Math.Ceiling(wizard2.LVL) : wizard2.LVL;
			int LVLup2 = Convert.ToInt32(Math.Floor(wizard2.LVL - lvlBefore2));
			wizard2.Health += LVLup2 * Wizard.LVLHeal;
		}
	}

	private static T ReadFromJson<T>(string filename)
	{
		return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename), _jsonSerializerSettings); //"Possible null reference return"
	}

	private static void TestEnviroment(Wizard wizard1)
	{
		wizard1.LVL = wizard1.Name == "t1"|| wizard1.Name == "t2" ? Wizard.MaxLVL : 1;
	}

	public static Wizard GetTarget(Spell spell, IUserInterface userInterface, Wizard self, Wizard enemy)
	{
		return spell.TargetType switch
		{
			TargetType.Select => userInterface.UserPicksTarget() == Target.Self ? self : enemy,
			TargetType.SelfOnly => self,
			TargetType.EnemyOnly => enemy
		};
	}

	public static void ResetWizard(Wizard wizard)
    {
		wizard.Health = 100;
		wizard.Mana = 100;
		TestEnviroment(wizard);
	}

	public static Wizard GetWizard(string PlayerNumber, IUserInterface userInterface)
    {
		userInterface.EnterPlayer(PlayerNumber);
		var PlayerName = userInterface.GetPromptedText(": ");
		return new Wizard(PlayerName);
	}
}