namespace WizardWars.Lib.Effects;

public class CounterEffect : Effect
{
	public int HealAmount { get; set; }
	public int RestoreManaAmount { get; set; }


	// TODO: needs work!

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{



		// TODO: rework trickery
		var enemySpellCast = playerSpell == turn.FirstPlayerSpell ? turn.SecondPlayerSpell : turn.FirstPlayerSpell;

		if (enemySpellCast.Spell.TriggerPhase > playerSpell.Spell.TriggerPhase && enemySpellCast.Spell.TriggerPhase <= playerSpell.Spell.StopPhase)
		{
			playerSpell.Continue = false;

			turn.AddLogMessage(new CounterEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				enemySpellCast.Spell.Name));
			
			if(HealAmount>0)
            {
				playerSpell.Caster.Health += HealAmount;
				turn.AddLogMessage(new SelfHealEventLogMessage(
					playerSpell.Caster.Name,
					playerSpell.Spell.Name,
					HealAmount));
			}
			if (RestoreManaAmount>0)
            {
				playerSpell.Caster.Mana += RestoreManaAmount;
				turn.AddLogMessage(new SelfRestoreManaEventLogMessage(
					playerSpell.Caster.Name,
					playerSpell.Spell.Name,
					RestoreManaAmount));
			}
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