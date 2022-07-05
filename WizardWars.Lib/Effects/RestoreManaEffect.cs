namespace WizardWars.Lib.Effects;

public class RestoreManaEffect : Effect //TODO
{
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{

		if (playerSpell.Target.Alive)
		{

			int TrueRestoreManaAmount = Math.Min(RestoreManaAmount, playerSpell.Target.MaxMana - playerSpell.Target.Mana);
		playerSpell.Target.Mana += TrueRestoreManaAmount;

		turn.AddLogMessage(new ManaGainEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			TrueRestoreManaAmount));
		}
		else
		{
			turn.AddLogMessage(new TargetAlreadyDeadEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				playerSpell.Spell.Name));
		}
	}
}