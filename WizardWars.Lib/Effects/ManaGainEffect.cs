namespace WizardWars.Lib.Effects;

public class ManaGainEffect : Effect
{
	public int ManaGainAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.Mana += ManaGainAmount;

		turn.AddLogMessage(new ManaGainEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			ManaGainAmount));
	}
}