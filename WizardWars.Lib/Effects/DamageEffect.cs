namespace WizardWars.Lib.Effects;

public class DamageEffect : Effect
{
	public int Damage { get; set; }

	public override void Apply(Wizard target)
	{
		target.Health -= Damage;
	}
}