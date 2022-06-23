namespace WizardWars.Lib.Effects;

public class HealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(Wizard target, Wizard wizard)
	{
		target.Health += HealAmount;
	}
}