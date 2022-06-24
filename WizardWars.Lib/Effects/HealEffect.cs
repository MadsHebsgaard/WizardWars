namespace WizardWars.Lib.Effects;

public class HealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell)
	{
		playerSpell.Target.Health += HealAmount;
	}
}