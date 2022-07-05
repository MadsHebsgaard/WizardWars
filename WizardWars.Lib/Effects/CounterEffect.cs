namespace WizardWars.Lib.Effects;

public class CounterEffect : Effect
{
	public int HealAmount { get; set; }
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		SpellTarget enemySpellCast = turn.PlayerSpellList.Single(x => x.Caster == playerSpell.Target);

		if (enemySpellCast.Spell.TriggerPhase > playerSpell.Spell.TriggerPhase && enemySpellCast.Spell.TriggerPhase <= playerSpell.Spell.StopPhase
		|| enemySpellCast.Spell.TriggerPhase == playerSpell.Spell.TriggerPhase && turn.PlayerSpellList.IndexOf(playerSpell) < turn.PlayerSpellList.IndexOf(enemySpellCast)) 
		{
			turn.AddLogMessage(new CounterEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				enemySpellCast.Spell.Name));

			enemySpellCast.Continue = false;

			//Heal and mana reward for countering spell
			if (HealAmount > 0)
			{
				playerSpell.Caster.Health += HealAmount;
				turn.AddLogMessage(new SelfHealEventLogMessage(
					playerSpell.Caster.Name,
					playerSpell.Spell.Name,
					HealAmount));
			}
			if (RestoreManaAmount > 0)
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