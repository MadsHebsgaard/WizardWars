namespace WizardWars.Lib.Effects;

public class CounterEffect : Effect
{
	// TODO: needs work!
	
	public override void Apply(SpellTarget playerSpell)
	{
		playerSpell.Continue = false;
	}
}