using Newtonsoft.Json;
using WizardWars.Lib;
using System.Collections.Generic;


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
		userInterface.DisplayWizardWars();
		int MaxWizards;


		
		List<Wizard> wizards = new List<Wizard>(); //TODO: make all this a function
		while (true)
		{
			Console.Write("How many wizards are dualing? ");
			string value = Console.ReadLine();
			bool success = int.TryParse(value, out MaxWizards);
			if(success) { break; }
            else { Console.WriteLine("\nWrite an integer..."); }
		}
		for (int wz = 0; wz < MaxWizards; wz++) 
        {
			wizards.Add(GetWizard($"{wz+1}. Player", userInterface));
		}

		int numberOfSpells = 12;

		while (true)
        {
			int AliveCount = MaxWizards;
			int wz1 = 0;

			//Duel setup
			Console.WriteLine();
			int turnNumber = 0, maxTurns = 100;

			foreach (var Wizard in wizards)
			{
				ResetWizard(Wizard);
				Wizard.Spellbook = userInterface.GetSpells(spellsFromJson, numberOfSpells, Wizard.Name);
			}

			List<Wizard> livingWizards = wizards;

			while (AliveCount>=2)
			{
				userInterface.DisplayTurnNumber(turnNumber);

				foreach (var Wizard in livingWizards)
				{
					userInterface.DisplayStatsGraph(Wizard);
				}
				Console.WriteLine();

				List <Spell> SpellTurn = new List<Spell> (new Spell[AliveCount]);
				List <Wizard> TargetTurn = new List<Wizard>(new Wizard[AliveCount]);
				List <SpellTarget> SpellTargetTurn = new List<SpellTarget>(new SpellTarget[AliveCount]);
				List<Wizard> WizardListOrdered = livingWizards.Skip(wz1).Concat(livingWizards.Take(wz1)).ToList();
				
				int i = 0;
				foreach (var Wizard in WizardListOrdered)
                {
                    SpellTurn[i] = userInterface.UserPicksSpell(Wizard, Wizard.Spellbook.Where(x => x.ManaCost < Wizard.Mana && x.HealthCost < Wizard.Health && x.LVLRequired <= Wizard.LVL).ToList());
                    TargetTurn[i] = GetTarget(userInterface, livingWizards);
                    SpellTargetTurn[i] = new SpellTarget(Wizard, SpellTurn[i], TargetTurn[i]);
					i++;
				}

				var turn = new Turn(SpellTargetTurn);
				turn.Execute();
				userInterface.DisplayEventLog(turn.EventLog);
				turnNumber++;

				livingWizards = livingWizards.Where(x => x.Alive).ToList();
				AliveCount = livingWizards.Count;


				UpdateStats(WizardListOrdered);

				wz1 = wz1 < AliveCount-1 ? wz1 + 1 : 0;
			}
			userInterface.DisplayWinText(wizards, turnNumber, maxTurns); //TODO: Fix this for n-player
		}
	}

	private static void UpdateStats(List <Wizard> WizardList)
    {
		double lvlBefore;
		foreach (var Wizard in WizardList)
		{
			lvlBefore = Math.Floor(Wizard.LVL);
			if (Wizard.LVL != Wizard.MaxLVL)
			{
				Wizard.LVLRegen = 1 / Math.Floor(Wizard.LVL + 1) + 0.1;
				Wizard.LVL = Wizard.LVL + Wizard.LVLRegen > Wizard.MaxLVL ? Wizard.MaxLVL : Math.Round(Wizard.LVL + Wizard.LVLRegen, 2);
				Wizard.LVL = Wizard.LVL % 1 > 0.95 ? Math.Ceiling(Wizard.LVL) : Wizard.LVL;
				int LVLup1 = Convert.ToInt32(Math.Floor(Wizard.LVL - lvlBefore));
				Wizard.Health += LVLup1 * Wizard.LVLHeal;
			}
			Wizard.Resistance = 0;
			Wizard.Health += Wizard.HealthRegen;
			Wizard.Mana += Wizard.ManaRegen;
			Wizard.Mana = Wizard.Mana < Wizard.MaxMana ? Wizard.Mana : Wizard.MaxMana;
			Wizard.Health = Wizard.Health < Wizard.MaxHealth ? Wizard.Health : Wizard.MaxHealth;
			Wizard.Mana = Wizard.Mana < 0 ? 0 : Wizard.Mana;
		}
	}

	private static T ReadFromJson<T>(string filename)
	{
		return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename), _jsonSerializerSettings); //"Possible null reference return"
	}

	private static void TestEnviroment(Wizard wizard)
	{
		wizard.LVL = wizard.Name == "t1"|| wizard.Name == "t2" || wizard.Name == "t3" || wizard.Name == "t4" ? Wizard.MaxLVL : 1;
	}

	public static Wizard GetTarget(IUserInterface userInterface, List <Wizard> WizardList)
	{
		return userInterface.UserPicksTarget(WizardList);
	}

	public static void ResetWizard(Wizard wizard)
    {
		wizard.Health = wizard.MaxHealth;
		wizard.Mana = wizard.MaxMana;
		wizard.Alive = true;
		TestEnviroment(wizard);
	}

	public static Wizard GetWizard(string PlayerNumber, IUserInterface userInterface)
    {
		userInterface.EnterPlayer(PlayerNumber);
		var PlayerName = userInterface.GetPromptedText(": ");
		return new Wizard(PlayerName);
	}
}