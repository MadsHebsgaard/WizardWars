using WizardWars.Lib;
using Spectre.Console;
namespace WizardWars.ConsoleApp;

public class SpectreConsoleUserInterface : IUserInterface
{
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

	public Spell UserPicksSpell(Wizard wizard, List<Spell> spells)
	{
			var spell = AnsiConsole.Prompt(
				new SelectionPrompt<Spell>()
					.Title($" {wizard.Name}, select your spell!")
					.UseConverter(x => x.Name)
					.AddChoices(spells));
		return spell;
	}

	public Target UserPicksTarget()
	{
		return AnsiConsole.Prompt(new SelectionPrompt<Target>()
			.Title("Who do you want to target?")
			.UseConverter(x => x.ToString())
			.AddChoices(Enum.GetValues<Target>()));
	}

	public void DisplaySpellCastInformation(SpellTarget spellTarget)
	{
		Console.WriteLine(spellTarget.Caster.Name + " used " + spellTarget.Spell.Name + " at " + spellTarget.Target.Name);
	}
	public void DisplayWinText(Wizard wizard1, Wizard wizard2, int turnNumber, int maxTurns)
	{
		if (turnNumber != maxTurns)
		{
			if (wizard1.Health > 0)	{ Console.WriteLine(wizard1.Name + " killed " + wizard2.Name + " and won the Duel!"); }
			else if (wizard2.Health > 0) { Console.WriteLine(wizard2.Name + " murdered " + wizard1.Name + " and won the Duel!"); }
			else { Console.WriteLine(wizard1.Name + " and " + wizard2.Name + " killed each other and the Duel is lost for both but their honors remain intact!"); }
		}
		else { Console.WriteLine("Dual over! Ran out of max turns of " + maxTurns);	}
	}

	public void DisplayEventLog(IReadOnlyList<IEventLogMessage> turnEventLog) //X used y on x -> X used y on himself
	{
		foreach (var message in turnEventLog)
		{
			switch (message)
			{
				case SpellCastLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/] casts [yellow]{spellEvent.SpellName}[/] on [purple]{spellEvent.Target}[/].");
					break;
				case DamageEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] deals [red]{spellEvent.Amount} damage[/] to [purple]{spellEvent.Target}[/].");
					break;
				case HealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] heals [green]{spellEvent.Amount} health[/] to [purple]{spellEvent.Target}[/].");
					break;
				case CounterEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/] counters [purple]{spellEvent.Target}[/]'s [yellow]{spellEvent.SpellName}[/]!");
					break;
				case ManaGainEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] replenishes [blue]{spellEvent.Amount} mana[/] to [purple]{spellEvent.Target}[/]");
					break;
				case LifeStealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] steals [green]{spellEvent.Amount} health[/] from [purple]{spellEvent.Target}[/].");
					break;
				case ManaStealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] steals [blue]{spellEvent.Amount} mana[/] from [purple]{spellEvent.Target}[/].");
					break;
				case SelfDamageEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] deals [red]{spellEvent.Amount} damage[/] to himself.");
					break;
				case SelfHealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] heals [green]{spellEvent.Amount} health[/] to himself.");
					break;
				case AreaHealEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] heals [green]{spellEvent.Amount} health[/] to everyone.");
					break;
				case RemoveManaEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [purple]{spellEvent.SpellName}[/] removes [blue]{spellEvent.Amount} mana[/] to [purple]{spellEvent.Target}[/]");
					break;
				case SelfRestoreManaEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [purple]{spellEvent.SpellName}[/] replenishes [blue]{spellEvent.Amount} mana[/] to himself");
					break;
				case LVLEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] gives [purple]{spellEvent.Amount} LVL[/] to [purple]{spellEvent.Target}[/]");
					break;
				case SelfLVLEventLogMessage spellEvent:
					AnsiConsole.MarkupLine($"[purple]{spellEvent.Source}[/]'s [yellow]{spellEvent.SpellName}[/] gives [purple]{spellEvent.Amount} LVL[/] to himself");
					break;
			}
		}
	}
	public void DisplayStatsGraph(Wizard wizard1, Wizard wizard2)
	{
		int x = Math.Max(wizard1.Name.Length, wizard2.Name.Length);


		AnsiConsole.Write(new BarChart()
		.Width(60)
		.Label($"[green bold underline] Wizard Stats[/]")
		.CenterLabel()
		.AddItem("", 100, Color.Black)
		.AddItem($" {wizard1.Name.PadRight(x)} health", wizard1.Health, Color.Green)
		.AddItem($" {wizard1.Name.PadRight(x)}   Mana", wizard1.Mana, Color.Blue));

		AnsiConsole.Write(new BarChart()
		.Width(59)
		.AddItem($" {wizard1.Name.PadRight(x)}    LVL", wizard1.LVL, Color.Purple)
		.AddItem("", 10, Color.Black));


		AnsiConsole.Write(new BarChart()
		.Width(60)
		.AddItem("", 100, Color.Black)
		.AddItem($" {wizard2.Name.PadRight(x)} health", wizard2.Health, Color.Green)
		.AddItem($" {wizard2.Name.PadRight(x)}   Mana", wizard2.Mana, Color.Blue));

		AnsiConsole.Write(new BarChart()
		.Width(59)
		.AddItem($" {wizard2.Name.PadRight(x)}    LVL", wizard2.LVL, Color.Purple)
		.AddItem("", 10, Color.Black));
	}
	public void DisplayTurnNumber(int turnNumber)
    {
		AnsiConsole.MarkupLine($"\n [darkorange]--------------------------------------------------------------\n	[bold]Turn: {turnNumber}[/]\n --------------------------------------------------------------[/]  ");

	}

}