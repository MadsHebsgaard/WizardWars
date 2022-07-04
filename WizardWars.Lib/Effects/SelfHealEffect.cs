namespace WizardWars.Lib.Effects;

public class SelfHealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		int HealthHealed = Math.Min(HealAmount, playerSpell.Target.MaxHealth - playerSpell.Target.Health);
		playerSpell.Caster.Health += HealthHealed;

		turn.AddLogMessage(new SelfHealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			HealthHealed));
	}
}