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
		table.AddColumn("IQ");

		table.AddRow(wizard1.Name, $"[red]{wizard1.Health}[/]", $"[blue]{wizard1.Mana}[/]", $"[green]{wizard1.IQ}[/]");
		table.AddRow(wizard2.Name, $"[red]{wizard2.Health}[/]", $"[blue]{wizard2.Mana}[/]", $"[green]{wizard2.IQ}[/]");

		AnsiConsole.Write(table);
	}

	public Spell UserPicksSpell(Wizard wizard, List<Spell> spells)
	{
			var spell = AnsiConsole.Prompt(
				new SelectionPrompt<Spell>()
					.Title(wizard.Name + ", select your spell!")
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
}