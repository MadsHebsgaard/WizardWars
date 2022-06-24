namespace WizardWars.Lib.Effects;

public class ManaRegenEffect : Effect
{
	public int ManaRegen { get; set; }

	public override void Apply(SpellTarget playerSpell)
	{
		playerSpell.Target.ManaRegen += ManaRegen;
	}
}