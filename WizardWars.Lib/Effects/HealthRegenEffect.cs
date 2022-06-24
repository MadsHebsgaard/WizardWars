namespace WizardWars.Lib.Effects;

public class HealthRegenEffect : Effect
{
	public int HealthRegenAmount { get; set; }

	public override void Apply(SpellTarget playerSpell)
	{
		playerSpell.Target.HealthRegen += HealthRegenAmount;
	}
}