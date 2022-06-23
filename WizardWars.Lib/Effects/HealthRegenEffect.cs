namespace WizardWars.Lib.Effects;

public class HealthRegenEffect : Effect
{
	public int HealthRegen { get; set; }

	public override void Apply(Wizard target, Wizard wizard)
	{
		target.HealthRegen += HealthRegen;
	}
}