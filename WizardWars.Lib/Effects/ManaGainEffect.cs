namespace WizardWars.Lib.Effects;

public class ManaGainEffect : Effect
{
	public int ManaGainAmount { get; set; }

	public override void Apply(SpellTarget playerSpell)
	{
		playerSpell.Target.Mana += ManaGainAmount;
	}
}