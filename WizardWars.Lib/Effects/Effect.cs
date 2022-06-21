namespace WizardWars.Lib.Effects;

public abstract class Effect
{
	public SpellPhase Phase { get; set; }
	public abstract void Apply(Wizard target);
}