namespace WizardWars.Lib.Effects;

public class LifeStealEffect : Effect
{
	public int LifeStealAmount { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		target.Health -= LifeStealAmount;
		caster.Health += LifeStealAmount;
	}
}