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
		foreach (var phase in Enum.GetValues<SpellPhase>().Skip(1))
		{
			if (SecondPlayerSpell.Continue) //Continue is a bool that I think should be true untill told otherwise
			{
				FirstPlayerSpell.Spell.ApplyEffects(phase, FirstPlayerSpell);
			}

			if (FirstPlayerSpell.Continue)
			{
				SecondPlayerSpell.Spell.ApplyEffects(phase, SecondPlayerSpell);
			}

			if (FirstPlayerSpell.Caster.Health <= 0 || SecondPlayerSpell.Caster.Health <= 0)
			{
				return;
			}
		}
	}
}