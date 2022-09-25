namespace WizardWars.Lib.Effects;

public class SelfLVLEffect : Effect
{
	public double LVLAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		double LvlGained = Math.Min(LVLAmount, Wizard.MaxLVL - playerSpell.Caster.LVL);
		int LvlUp = Convert.ToInt32(Math.Floor((playerSpell.Caster.LVL % 1) + LvlGained));

		playerSpell.Caster.LVL += LvlGained;
		playerSpell.Caster.Health += LvlUp * Wizard.LVLHeal;

		turn.AddLogMessage(new SelfLVLEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			LvlGained));
	}
}