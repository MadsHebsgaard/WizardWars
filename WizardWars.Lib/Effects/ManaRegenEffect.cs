namespace WizardWars.Lib.Effects;

public class ManaRegenEffect : Effect
{
	public int ManaRegen { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		target.ManaRegen += ManaRegen;
	}
}