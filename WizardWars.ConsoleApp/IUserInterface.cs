using WizardWars.Lib;

namespace WizardWars.ConsoleApp;

public interface IUserInterface
{
	public void DisplayWizardWars();
	public void EnterPlayer(string player);
	string GetPromptedText(string prompt);
	void DisplayStats(Wizard wizard1, Wizard wizard2);
	Spell UserPicksSpell(Wizard wizard, List<Spell> spells);
	Wizard UserPicksTarget(List<Wizard> WizardList);
	void DisplayWinText(Wizard wizard1, Wizard wizard2, int turnNumber, int maxTurns, int wz1);
	void DisplayEventLog(IReadOnlyList<IEventLogMessage> turnEventLog);
	public void DisplayTurnNumber(int turnNumber);
	public List<Spell> GetSpells(List<Spell> spells, int numberOfSpells, string name);
	public void DisplayStatsGraph(Wizard wizard);

}