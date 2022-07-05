namespace WizardWars.Lib.Effects;

public class HealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		if (playerSpell.Target.Alive)
		{
			int HealthHealed = Math.Min(HealAmount, playerSpell.Target.MaxHealth - playerSpell.Target.Health);
			playerSpell.Target.Health += HealthHealed;

			turn.AddLogMessage(new HealEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				playerSpell.Spell.Name,
				HealthHealed));
		}
		else
		{
			turn.AddLogMessage(new TargetAlreadyDeadEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				playerSpell.Spell.Name));
		}
	}
}
