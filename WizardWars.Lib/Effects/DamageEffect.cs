namespace WizardWars.Lib.Effects;

public class DamageEffect : Effect
{
	public int DamageAmount { get; set; }


	public override void Apply(SpellTarget playerSpell)
	{
		playerSpell.Target.Health -= DamageAmount;
	}
}