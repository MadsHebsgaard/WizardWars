using WizardWars.Lib;

namespace WizardWars.ConsoleApp;

public interface IUserInterface
{
	string GetPromptedText(string prompt);
	void DisplayStats(Wizard wizard1, Wizard wizard2);
	Spell UserPicksSpell(Wizard wizard, List<Spell> spells);
	Target UserPicksTarget();
	void DisplaySpellCastInformation(SpellTarget p1);
	void DisplayWinText(Wizard wizard1, Wizard wizard2, int turnNumber, int maxTurns);
	void DisplayEventLog(IReadOnlyList<IEventLogMessage> turnEventLog);
}