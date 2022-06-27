namespace WizardWars.Lib.Effects;

public class CounterEffect : Effect
{
	// TODO: needs work!
	
	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Continue = false;



		// TODO: rework trickery
		var enemySpellCast = playerSpell == turn.FirstPlayerSpell ? turn.SecondPlayerSpell : turn.FirstPlayerSpell;

		if (enemySpellCast.Spell.TriggerPhase != SpellPhase.One)
		{
			turn.AddLogMessage(new CounterEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				enemySpellCast.Spell.Name));
		}
		else
        {
			turn.AddLogMessage(new FailCounterEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				enemySpellCast.Spell.Name));
		}
	}
}