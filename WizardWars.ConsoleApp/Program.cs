﻿using Newtonsoft.Json;
using WizardWars.Lib;
namespace WizardWars.ConsoleApp;

public static class Program
{
	private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
	{
		TypeNameHandling = TypeNameHandling.Auto
	};

	public static void Main()
	{
		//UI and Spell setup
		IUserInterface userInterface = new SpectreConsoleUserInterface();
		var spellsFromJson = ReadFromJson<IEnumerable<Spell>>("spells.json").ToList();
		userInterface.DisplayWizardWars();
		List<Wizard> wizards = GetWizards(userInterface);
		int numberOfSpells = 12;

		while (true)
        {
			int AliveCount = wizards.Count;
			int wz1 = 0;
			int turnNumber = 0, maxTurns = 100;

			foreach (var Wizard in wizards)
			{
				ResetWizard(Wizard);
				Wizard.Spellbook = userInterface.GetSpells(spellsFromJson, numberOfSpells, Wizard.Name);
			}
			List<Wizard> livingWizards = wizards;
			Console.WriteLine();
			
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
                    SpellTurn[i] = userInterface.UserPicksSpell(Wizard, Wizard.Spellbook.Where(x => x.ManaCost <= Wizard.Mana && x.HealthCost < Wizard.Health && x.LVLRequired <= Wizard.LVL).ToList());
                    TargetTurn[i] = GetTarget(userInterface, livingWizards, Wizard, SpellTurn[i].TargetType);
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
			userInterface.DisplayWinText(wizards, turnNumber, maxTurns);
		}
	}
	private static void UpdateStats(List <Wizard> wizardList)
    {
		foreach (var Wizard in wizardList)
		{
			double lvlBefore = Math.Floor(Wizard.LVL);
			if (Wizard.LVL != Wizard.MaxLVL)
			{
				Wizard.LVLRegen = 1 / Math.Floor(Wizard.LVL + 1) + 0.1;
				Wizard.LVL = Wizard.LVL + Wizard.LVLRegen > Wizard.MaxLVL ? Wizard.MaxLVL : Math.Round(Wizard.LVL + Wizard.LVLRegen, 2);
				Wizard.LVL = Wizard.LVL % 1 > 0.95 ? Math.Ceiling(Wizard.LVL) : Wizard.LVL;
				int lvlUp = Convert.ToInt32(Math.Floor(Wizard.LVL - lvlBefore));
				Wizard.Health += lvlUp * Wizard.LVLHeal;
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
	private static void TestEnvironment(Wizard wizard)
	{
		wizard.LVL = wizard.Name == "t1"|| wizard.Name == "t2" || wizard.Name == "t3" || wizard.Name == "t4" ? Wizard.MaxLVL : 1;
	}
	private static Wizard GetTarget(IUserInterface userInterface, List <Wizard> wizardList, Wizard self, TargetType targetType)
	{
			return targetType switch
			{
				TargetType.Select => userInterface.UserPicksTarget(wizardList),
				TargetType.EnemyOnly => userInterface.UserPicksTarget(wizardList.Where(x => x != self).ToList()),
				TargetType.SelfOnly => self,
				TargetType.AOE => self
			};
		//return userInterface.UserPicksTarget(WizardList);
	}
	private static void ResetWizard(Wizard wizard)
    {
		wizard.Health = wizard.MaxHealth;
		wizard.Mana = wizard.MaxMana;
		wizard.Alive = true;
		TestEnvironment(wizard);
	}
	private static Wizard GetWizard(string playerNumber, IUserInterface userInterface)
    {
		userInterface.EnterPlayer(playerNumber);
		var PlayerName = userInterface.GetPromptedText(": ");
		return new Wizard(PlayerName);
	}


	private static List<Wizard> GetWizards(IUserInterface userInterface)
	{
		int MaxWizards;
		List<Wizard> wizards = new List<Wizard>();
		
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

		return wizards;
	}
}