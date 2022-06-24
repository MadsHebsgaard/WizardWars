namespace WizardWars.Lib.Effects;

public class ManaStealEffect : Effect
{
	public int ManaSteal { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		caster.Mana += ManaSteal;
		target.Mana -= ManaSteal;
	}
}