namespace WizardWars.Lib.Effects;

public class ManaStealEffect : Effect
{
	public int ManaStealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{

		if (playerSpell.Target.Alive)
		{
			playerSpell.Caster.Mana += ManaStealAmount;
		playerSpell.Target.Mana -= ManaStealAmount;

		turn.AddLogMessage(new ManaStealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			ManaStealAmount));
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