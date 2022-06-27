namespace WizardWars.Lib.Effects;

public class SelfRestoreManaEffect : Effect
{
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Caster.Mana += RestoreManaAmount;

		turn.AddLogMessage(new SelfRestoreManaEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			RestoreManaAmount));
	}
}