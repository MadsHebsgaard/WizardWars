namespace WizardWars.Lib.Effects;

public class BounceEffect : Effect
{
	
	public override void Apply(Wizard caster, Wizard target)
	{
		target = caster; //TODO doesn't work
	}
}