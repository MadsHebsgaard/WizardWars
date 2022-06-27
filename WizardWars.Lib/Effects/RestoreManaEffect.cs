namespace WizardWars.Lib.Effects;

public class ManaGaiqnEffect : Effect
{
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.Mana += RestoreManaAmount;

		turn.AddLogMessage(new ManaGainEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			RestoreManaAmount));
	}
}