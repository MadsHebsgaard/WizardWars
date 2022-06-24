namespace WizardWars.Lib.Effects;

public class HealthRegenEffect : Effect
{
	public int HealthRegenAmount { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		target.HealthRegen += HealthRegenAmount;
	}
}