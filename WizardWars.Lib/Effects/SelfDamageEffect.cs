namespace WizardWars.Lib.Effects;

public class SelfDamageEffect : Effect
{
	public int SelfDamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Caster.Health -= SelfDamageAmount;

		turn.AddLogMessage(new SelfDamageEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			SelfDamageAmount));
	}
}