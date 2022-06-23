namespace WizardWars.Lib.Effects;

public class HealthRegenEffect : Effect
{
	public int HealthRegen { get; set; }

	public override void Apply(Wizard target)
	{
		target.HealthRegen += HealthRegen;
	}
}