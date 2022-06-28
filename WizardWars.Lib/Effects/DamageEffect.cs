namespace WizardWars.Lib.Effects;

public class DamageEffect : Effect
{
	public int DamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		if (playerSpell.Target.Resistance > 0)
        {
			int BlockAmount = Convert.ToInt32(DamageAmount * playerSpell.Target.Resistance);
			var enemySpellCast = playerSpell == turn.FirstPlayerSpell ? turn.SecondPlayerSpell : turn.FirstPlayerSpell;
			turn.AddLogMessage(new ResistanceEventLogMessage(
				playerSpell.Caster.Name,
				enemySpellCast.Caster.Name,
				enemySpellCast.Spell.Name,
				BlockAmount));
			DamageAmount -= BlockAmount;
		}

		playerSpell.Target.Health -= DamageAmount;

		turn.AddLogMessage(new DamageEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			DamageAmount));
	}
}