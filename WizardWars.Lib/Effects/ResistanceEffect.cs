namespace WizardWars.Lib.Effects;

public class ResistanceEffect : Effect
{
	public double ResistanceAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.Resistance += ResistanceAmount;
	}
}