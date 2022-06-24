namespace WizardWars.Lib.Effects;

public class HealthRegenEffect : Effect
{
	public int HealthRegenAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.HealthRegen += HealthRegenAmount;
	}
}