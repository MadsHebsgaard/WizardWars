namespace WizardWars.Lib.Effects;

public class DamageEffect : Effect
{
	public int DamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.Health -= DamageAmount;

		turn.AddLogMessage(new DamageEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			DamageAmount));
	}
}