namespace WizardWars.Lib;

public class Turn
{
	public SpellTarget FirstPlayerSpell { get; }
	public SpellTarget SecondPlayerSpell { get; }

	public Turn(SpellTarget firstPlayerSpell, SpellTarget secondPlayerSpell)
	{
		FirstPlayerSpell = firstPlayerSpell;
		SecondPlayerSpell = secondPlayerSpell;
	}

	public void Execute()
	{
		foreach (var phase in Enum.GetValues<SpellPhase>())
		{
			if (FirstPlayerSpell.Continue)
			{
				FirstPlayerSpell.Spell.ApplyEffects(phase, FirstPlayerSpell.Target);
			}
		}
	}
}