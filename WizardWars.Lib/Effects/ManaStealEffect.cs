namespace WizardWars.Lib.Effects;

public class ManaStealEffect : Effect
{
	public int ManaSteal { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Caster.Mana += ManaSteal;
		playerSpell.Target.Mana -= ManaSteal;
	}
}