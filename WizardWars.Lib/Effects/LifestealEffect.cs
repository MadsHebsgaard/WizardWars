namespace WizardWars.Lib.Effects;

public class LifestealEffect : Effect
{
	public int LifestealAmount { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		target.Health -= LifestealAmount;
		caster.Health += LifestealAmount;
	}
}