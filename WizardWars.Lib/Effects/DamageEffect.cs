namespace WizardWars.Lib.Effects;

public class DamageEffect : Effect
{
	public int DamageAmount { get; set; }

	public override void Apply(Wizard target)
	{
		target.Health -= DamageAmount;
	}
}