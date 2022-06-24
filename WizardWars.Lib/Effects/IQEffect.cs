namespace WizardWars.Lib.Effects;

public class IQEffect : Effect
{
	public int IQAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.IQ += IQAmount;
	}
}