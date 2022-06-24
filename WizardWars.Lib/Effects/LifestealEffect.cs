namespace WizardWars.Lib.Effects;

public class LifeStealEffect : Effect
{
	public int LifeStealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.Health -= LifeStealAmount;
		playerSpell.Caster.Health += LifeStealAmount;
	}
}