using WizardWars.Lib;

namespace WizardWars.ConsoleApp;

public class ConsoleUserInterface : IUserInterface
{
	public string GetPromptedText(string prompt)
	{
		Console.Write(prompt);
		return Console.ReadLine() ?? string.Empty;
	}

	public void DisplayStats(Wizard wizard1, Wizard wizard2)
	{
		var s = Math.Max(wizard1.Name.Length, wizard2.Name.Length);

		Console.WriteLine(wizard1.Name.PadRight(s) + ": Hp = " + wizard1.Health + "  Mana = " + wizard1.Mana);
		Console.WriteLine(wizard2.Name.PadRight(s) + ": Hp = " + wizard2.Health + "  Mana = " + wizard2.Mana);
	}

	public Spell UserPicksSpell(Wizard wizard, List<Spell> spells)
	{
		Console.WriteLine($"Time for {wizard.Name} to pick a spell.");

		while (true)
		{
			foreach (var spell in spells)
			{
				Console.WriteLine(spell.Name);
			}

			var spellName = GetPromptedText("Write a spells name: ");

			var matches = spells.Where(x => x.Name.StartsWith(spellName, StringComparison.OrdinalIgnoreCase))
				.ToList();

			if (matches.Count <= 1)
				return matches.First();

			Console.WriteLine("Be more specific!");
		}
	}

	public Target UserPicksTarget()
	{
		Console.WriteLine("Pick your target.");

		foreach (var t in Enum.GetNames<Target>())
		{
			Console.WriteLine(t);
		}

		while (true)
		{
			var picked = GetPromptedText("Who is your target: ");

			if (Enum.TryParse<Target>(picked, true, out var result))
			{
				return result;
			}

			Console.WriteLine("Bad value, try again.");
		}
	}
}