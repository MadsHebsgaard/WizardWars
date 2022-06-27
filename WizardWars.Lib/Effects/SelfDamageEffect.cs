namespace WizardWars.Lib.Effects;

public class SelfDamageEffect : Effect
{
	public int DamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Caster.Health -= DamageAmount;

		turn.AddLogMessage(new SelfDamageEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			DamageAmount));
	}
}