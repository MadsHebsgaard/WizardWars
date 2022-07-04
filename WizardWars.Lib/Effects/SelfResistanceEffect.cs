namespace WizardWars.Lib.Effects;

public class SelfResistanceEffect : Effect
{
	public double ResistanceAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Caster.Resistance += ResistanceAmount;

		turn.AddLogMessage(new SelfResistanceEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			ResistanceAmount));
	}
}