namespace WizardWars.Lib.Effects;

public class DamageEffect : Effect
{
	public int DamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		int BlockAmount = 0;
		int DamageThrough = DamageAmount;
		if (playerSpell.Target.Resistance > 0)
        {
			BlockAmount = Convert.ToInt32(DamageAmount * playerSpell.Target.Resistance);
			var enemySpellCast = playerSpell == turn.FirstPlayerSpell ? turn.SecondPlayerSpell : turn.FirstPlayerSpell;
			turn.AddLogMessage(new ResistanceEventLogMessage(
				enemySpellCast.Caster.Name,
				playerSpell.Caster.Name,
				playerSpell.Spell.Name,
				BlockAmount));
		}
		DamageThrough -= BlockAmount;
		playerSpell.Target.Health -= DamageThrough;

		turn.AddLogMessage(new DamageEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			DamageThrough));
	}
}