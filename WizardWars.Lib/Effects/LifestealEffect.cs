namespace WizardWars.Lib.Effects;

public class LifeStealEffect : Effect
{
	public int LifeStealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell)
	{
		playerSpell.Target.Health -= LifeStealAmount;
		playerSpell.Caster.Health += LifeStealAmount;
	}
}