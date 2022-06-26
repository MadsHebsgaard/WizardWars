namespace WizardWars.Lib.Effects;

public class LVLEffect : Effect
{
	public int LVLAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.LVL += LVLAmount;
	}
}