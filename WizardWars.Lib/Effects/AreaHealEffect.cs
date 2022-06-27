namespace WizardWars.Lib.Effects;

public class AreaHealEffect : Effect
{
	public int AreaHealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		turn.SecondPlayerSpell.Caster.Health += AreaHealAmount;
		turn.FirstPlayerSpell.Caster.Health += AreaHealAmount;

		turn.AddLogMessage(new AreaHealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			AreaHealAmount));
	}
}