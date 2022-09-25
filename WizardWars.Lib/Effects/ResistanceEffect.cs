namespace WizardWars.Lib.Effects;

public class ResistanceEffect : Effect
{
	public double ResistanceAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		
		if (ResistanceAmount != 0)
		{
			playerSpell.Target.Resistance += ResistanceAmount;

			turn.AddLogMessage(new ResistanceEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				playerSpell.Spell.Name,
				ResistanceAmount));
		}
	}
}
