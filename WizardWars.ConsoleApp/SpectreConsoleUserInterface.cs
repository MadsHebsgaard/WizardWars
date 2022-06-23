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
		table.AddColumn("Knowledge");

		table.AddRow(wizard1.Name, $"[red]{wizard1.Health}[/]", $"[blue]{wizard1.Mana}[/]", $"[green]{wizard1.Knowledge}[/]");
		table.AddRow(wizard2.Name, $"[red]{wizard2.Health}[/]", $"[blue]{wizard2.Mana}[/]", $"[green]{wizard2.Knowledge}[/]");

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
}