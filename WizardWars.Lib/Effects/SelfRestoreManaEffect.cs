namespace WizardWars.Lib.Effects;

public class SelfRestoreManaEffect : Effect
{
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		int TrueRestoreManaAmount = Math.Min(RestoreManaAmount, playerSpell.Target.MaxMana - playerSpell.Target.Mana);
		playerSpell.Caster.Mana += TrueRestoreManaAmount;

		turn.AddLogMessage(new SelfRestoreManaEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			TrueRestoreManaAmount));
	}
}