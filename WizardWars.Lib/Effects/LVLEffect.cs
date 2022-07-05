namespace WizardWars.Lib.Effects;

public class LVLEffect : Effect
{
	public int LVLAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{

		if (playerSpell.Target.Alive)
		{
			double LvlGained = Math.Min(LVLAmount, Wizard.MaxLVL - playerSpell.Target.LVL);

		int LvlUp = Convert.ToInt32(Math.Floor((playerSpell.Target.LVL % 1) + LvlGained));
		playerSpell.Target.Health += LvlUp * Wizard.LVLHeal;
		playerSpell.Target.LVL += LvlGained;

		turn.AddLogMessage(new LVLEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			LvlGained));
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