namespace WizardWars.Lib.Effects;

public class RemoveManaEffect : Effect
{
	public int RemoveManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		if (playerSpell.Target.Alive)
		{
			playerSpell.Target.Mana -= RemoveManaAmount;

		turn.AddLogMessage(new RemoveManaEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			RemoveManaAmount));
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