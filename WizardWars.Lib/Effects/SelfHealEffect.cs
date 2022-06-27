namespace WizardWars.Lib.Effects;

public class SelfHealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Caster.Health += HealAmount;

		turn.AddLogMessage(new SelfHealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			HealAmount));
	}
}