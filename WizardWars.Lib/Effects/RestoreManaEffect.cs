namespace WizardWars.Lib.Effects;

public class ManaGaiqnEffect : Effect
{
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		int TrueRestoreManaAmount = Math.Min(RestoreManaAmount, playerSpell.Target.MaxMana - playerSpell.Target.Mana);
		playerSpell.Target.Mana += TrueRestoreManaAmount;

		turn.AddLogMessage(new ManaGainEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			TrueRestoreManaAmount));
	}
}