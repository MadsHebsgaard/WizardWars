namespace WizardWars.Lib.Effects;

public class AreaRestoreManaEffect : Effect
{
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		turn.SecondPlayerSpell.Caster.Mana += RestoreManaAmount;
		turn.FirstPlayerSpell.Caster.Mana += RestoreManaAmount;

		turn.AddLogMessage(new AreaRestoreManaEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			RestoreManaAmount));
	}
}