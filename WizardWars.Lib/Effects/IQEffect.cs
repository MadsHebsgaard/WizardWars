namespace WizardWars.Lib.Effects;

public class IQEffect : Effect
{
	public int IQAmount { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		target.IQ += IQAmount;
	}
}