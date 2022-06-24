namespace WizardWars.Lib.Effects;

public class ManaGainEffect : Effect
{
	public int ManaGainAmount { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		target.Mana += ManaGainAmount;
	}
}