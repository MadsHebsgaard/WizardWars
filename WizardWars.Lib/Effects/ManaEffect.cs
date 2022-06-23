namespace WizardWars.Lib.Effects;

public class ManaEffect : Effect
{
	public int ManaAmount { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		target.Mana += ManaAmount;
	}
}