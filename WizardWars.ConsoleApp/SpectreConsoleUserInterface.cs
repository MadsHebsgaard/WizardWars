﻿using WizardWars.Lib;
using Spectre.Console;

namespace WizardWars.ConsoleApp;

public class SpectreConsoleUserInterface : IUserInterface
{
	public void DisplayWizardWars()
	{
		AnsiConsole.Write(
			new FigletText("WizardWars")
				.LeftAligned()
				.Color(Color.Purple_2));
	}

	public void EnterPlayer(string player)
	{
		AnsiConsole.Markup($"Enter [bold purple_2]{player}[/] name");
	}

	public string GetPromptedText(string prompt)
	{
		return AnsiConsole.Ask<string>(prompt);
	}

	public void DisplayStats(Wizard wizard1, Wizard wizard2)
	{
		var table = new Table();

		table.AddColumn("Name");
		table.AddColumn("Health");
		table.AddColumn("Mana");
		table.AddColumn("LVL");

		table.AddRow(wizard1.Name, $"[red]{wizard1.Health}[/]", $"[blue]{wizard1.Mana}[/]", $"[green]{wizard1.LVL}[/]");
		table.AddRow(wizard2.Name, $"[red]{wizard2.Health}[/]", $"[blue]{wizard2.Mana}[/]", $"[green]{wizard2.LVL}[/]");

		AnsiConsole.Write(table);
	}

	/*public static List UserPicksSpellbook(IUserInterface userInterface)
	{
		return new List(Spellbook);
	} */



	public List<Spell> GetSpells(List<Spell> spells, int numberOfSpells, string name)
	{
		string AllSpells = AnsiConsole.Prompt(
		new SelectionPrompt<string>()
			.Title($"\n[purple]{name}[/], are you playing with all spells?")
			.AddChoices("No")
			.AddChoices("Yes"));
		if (AllSpells == "Yes") { return spells; }

		while (true)
		{
			var selectedSpells = AnsiConsole.Prompt(
				new MultiSelectionPrompt<Spell>()
					.Title($"\n [purple_2]{name}[/], pick up to {numberOfSpells} [yellow]spells[/] from the list.")
					.Mode(SelectionMode.Leaf)
					.Required()
					.PageSize(50)
					.UseConverter(x => x.Name)
					.AddChoiceGroup(new Spell() { Name = "Damage" },
						spells.Where(x => x.SpellType == SpellType.Damage))
					.AddChoiceGroup(new Spell() { Name = "Support" },
						spells.Where(x => x.SpellType == SpellType.Support))
					.AddChoiceGroup(new Spell() { Name = "Utility" },
						spells.Where(x => x.SpellType == SpellType.Utility))
					.AddChoiceGroup(new Spell() { Name = "Other" },
						spells.Where(x => x.SpellType == SpellType.Other)));

			if (selectedSpells.Count == numberOfSpells)
			{
				return selectedSpells;
			}
			if (selectedSpells.Count < numberOfSpells)
			{
				string aws = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title($" You picked less than {numberOfSpells} [yellow]spells[/]. Continue?")
					.AddChoices("Yes")
					.AddChoices("No"));
                if (aws == "Yes") { return selectedSpells;  }
			}
			if (selectedSpells.Count > numberOfSpells)
			{
				string aws = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title($" You picked more than than {numberOfSpells} [yellow]spells[/]. Try again?")
					.AddChoices("Yes"));
			}
		}
	}

	public Spell UserPicksSpell(Wizard wizard, List<Spell> spells)
	{
		var spell = AnsiConsole.Prompt(
			new SelectionPrompt<Spell>()
				.Title($" [bold purple_2]{wizard.Name}[/], select your [yellow]spell[/]!")
				.UseConverter(x => x.Name)
				.AddChoices(spells)
				.MoreChoicesText("[silver](Move up and down to reveal more [yellow]spells[/])[/]")
				.PageSize(50)
			//.HighlightStyle(Style().Foreground(Color.Fuchsia))
		);

		return spell;
	}

	public Target UserPicksTarget()
	{
		return AnsiConsole.Prompt(new SelectionPrompt<Target>()
			.Title("Who do you want to target?")
			.UseConverter(x => x.ToString())
			.AddChoices(Enum.GetValues<Target>()));
	}

	public void DisplayWinText(Wizard wizard1, Wizard wizard2, int turnNumber, int maxTurns)
	{
		if (turnNumber != maxTurns)
		{
			Console.WriteLine();
			if (wizard1.Health > 0)
			{
				AnsiConsole.MarkupLine(
					$" [bold purple_2]{wizard1.Name}[/] killed [bold purple_2]{wizard2.Name}[/] and won the Duel!");
				wizard1.WinCount++;
			}
			else if (wizard2.Health > 0)
			{
				AnsiConsole.MarkupLine(
					$" [bold purple_2]{wizard2.Name}[/] murdered [bold purple_2]{wizard1.Name}[/] and won the Duel!");
				wizard2.WinCount++;
			}
			else
			{
				AnsiConsole.MarkupLine(
					$" [bold purple_2]{wizard1.Name}[/] and [bold purple_2]{wizard2.Name}[/] killed each other and the Duel is lost for both but their honors remain intact!");
			}
		}
		else
		{
			Console.WriteLine(" Dual over! Ran out of max turns of " + maxTurns);
		}

		var rule0= new Rule("");
		var rule = new Rule($"[bold purple_2]{wizard1.Name}[/] [yellow]{wizard1.WinCount} - {wizard2.WinCount}[/] [bold purple_2]{wizard2.Name}[/]");
		rule.Style = Style.Parse("purple_2 bold");
		rule0.Style = Style.Parse("purple_2 bold");
		AnsiConsole.Write(rule0);
		AnsiConsole.Write(rule);
		AnsiConsole.Write(rule0);
	}

	public void DisplayEventLog(IReadOnlyList<IEventLogMessage> turnEventLog)
	{
		foreach (var message in turnEventLog)
		{
			switch (message)
			{
				case SpellCastLogMessage spellEvent:
				{
					AnsiConsole.Markup(
						$" [purple_2]{spellEvent.Source}[/] casts [yellow]{spellEvent.SpellName}[/] on [purple_2]{spellEvent.Target}[/] ");
					if (spellEvent.HealthCost > 0 && spellEvent.ManaCost > 0)
					{
						AnsiConsole.MarkupLine(
							$"for [green]{spellEvent.HealthCost} health[/] and [blue]{spellEvent.ManaCost} mana[/].");
					}
					else if (spellEvent.ManaCost > 0)
					{
						AnsiConsole.MarkupLine($"for [blue]{spellEvent.ManaCost} mana[/].");
					}
					else if (spellEvent.HealthCost > 0)
					{
						AnsiConsole.MarkupLine($"for [green]{spellEvent.HealthCost} health[/].");
					}
					else
					{
						Console.WriteLine("for free.");
					}
				}
					break;
				case DamageEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] deals [red]{spellEvent.Amount} damage[/] to [purple_2]{spellEvent.Target}[/].");
					break;
				case HealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] heals [green]{spellEvent.Amount} health[/] to [purple_2]{spellEvent.Target}[/].");
					break;
				case CounterEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/] counters [purple_2]{spellEvent.Target}[/]'s [yellow]{spellEvent.SpellName}[/]!");
					break;
				case FailCounterEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/] fails to counter [purple_2]{spellEvent.Target}[/]'s [yellow]{spellEvent.SpellName}[/]!");
					break;
				case RedirectEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/] redirects [purple_2]{spellEvent.Target}[/]'s [yellow]{spellEvent.SpellName}[/]!");
					break;
				case FailRedirectEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/] fails to redirect [purple_2]{spellEvent.Target}[/]'s [yellow]{spellEvent.SpellName}[/]!");
					break;
				case ResistanceEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/] blocks [red]{spellEvent.Amount} damage[/] from [purple_2]{spellEvent.Target}[/]'s [yellow]{spellEvent.SpellName}[/]!");
					break;
				case ManaGainEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] replenishes [blue]{spellEvent.Amount} mana[/] to [purple_2]{spellEvent.Target}[/]");
					break;
				case LifeStealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] deals [red]{spellEvent.Amount} damage[/] to [purple_2]{spellEvent.Target}[/] and heals [green]{spellEvent.Amount} health[/] to [purple_2]{spellEvent.Source}[/]");
					break;
				case ManaStealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] removes [blue]{spellEvent.Amount} mana[/] from [purple_2]{spellEvent.Target}[/] and replenishes [blue]{spellEvent.Amount} mana[/] to [purple_2]{spellEvent.Source}[/]");
					break;
				case SelfDamageEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] deals [red]{spellEvent.Amount} damage[/] to himself.");
					break;
				case SelfHealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] heals [green]{spellEvent.Amount} health[/] to himself.");
					break;
				case AreaRestoreManaEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] restores [blue]{spellEvent.Amount} mana[/] to everyone.");
					break;
				case AreaHealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] heals [green]{spellEvent.Amount} health[/] to everyone.");
					break;
				case RemoveManaEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [purple_2]{spellEvent.SpellName}[/] removes [blue]{spellEvent.Amount} mana[/] from [purple_2]{spellEvent.Target}[/]");
					break;
				case SelfRestoreManaEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [purple_2]{spellEvent.SpellName}[/] replenishes [blue]{spellEvent.Amount} mana[/] to himself");
					break;
				case LVLEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] gives [purple_2]{spellEvent.Amount} LVL[/] to [purple_2]{spellEvent.Target}[/]");
					break;
				case SelfLVLEventLogMessage spellEvent:
					AnsiConsole.MarkupLine(
						$" [purple_2]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] grants [purple_2]{spellEvent.Amount} LVL[/] to himself");
					break;
			}
		}
	}

	public void DisplayStatsGraph(Wizard wizard1, Wizard wizard2)
	{
		int x = Math.Max(wizard1.Name.Length, wizard2.Name.Length);

		AnsiConsole.Write(new BarChart()
			.Width(60)
			.WithMaxValue(100)
			.CenterLabel()
			.Label($"[green bold underline]Wizard Stats[/]\n")
			.AddItem($" [bold purple_2]{wizard1.Name.PadRight(x)}[/] [green]Health[/]", wizard1.Health, Color.Green)
			.AddItem($" [bold purple_2]{wizard1.Name.PadRight(x)}[/]   [blue]Mana[/]", wizard1.Mana, Color.Blue));

		AnsiConsole.Write(new BarChart()
			.Width(59)
			.WithMaxValue(10)
			.AddItem($" [bold purple_2]{wizard1.Name.PadRight(x)}[/]    [purple_2]LVL[/]", wizard1.LVL, Color.Purple));

		Console.WriteLine();
		AnsiConsole.Write(new BarChart()
			.Width(60)
			.WithMaxValue(100)
			.AddItem($" [bold purple_2]{wizard2.Name.PadRight(x)}[/] [green]Health[/]", wizard2.Health, Color.Green)
			.AddItem($" [bold purple_2]{wizard2.Name.PadRight(x)}[/]   [blue]Mana[/]", wizard2.Mana, Color.Blue));

		AnsiConsole.Write(new BarChart()
			.Width(59)
			.WithMaxValue(10)
			.AddItem($" [bold purple_2]{wizard2.Name.PadRight(x)}[/]    [purple_2]LVL[/]", wizard2.LVL, Color.Purple));
	}

	public void DisplayTurnNumber(int turnNumber)
	{
		var rule = new Rule($"[darkorange bold]Turn: { turnNumber}[/]");
		AnsiConsole.Write(rule);
	}
	public void DisplayLvlUp(string name, int lvl, int heal)
    {
		AnsiConsole.MarkupLine($" [purple_2]{name}[/] LVL's up to LVL {lvl}, and heals [green]{heal} health[/].");
	}
}