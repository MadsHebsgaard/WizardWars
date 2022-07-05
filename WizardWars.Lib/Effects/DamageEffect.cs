namespace WizardWars.Lib.Effects;

public class DamageEffect : Effect
{
	public int DamageAmount { get; set; }
	public int TrueDamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		if(playerSpell.Target.Alive)
        {

			int BlockAmount = 0;
			if (playerSpell.Target.Resistance != 0)
			{
				BlockAmount = Convert.ToInt32(DamageAmount * playerSpell.Target.Resistance);
				turn.AddLogMessage(new BlockEventLogMessage(
					playerSpell.Target.Name,
					playerSpell.Caster.Name,
					playerSpell.Spell.Name,
					BlockAmount));
			}
			int DamageTaken = TrueDamageAmount + DamageAmount - BlockAmount;

			playerSpell.Target.Health -= DamageTaken;

			turn.AddLogMessage(new DamageEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Target.Name,
				playerSpell.Spell.Name,
				DamageTaken));

			if (playerSpell.Target.Health <= 0) //Dead wizard check
			{
				playerSpell.Target.Health = 0;
				playerSpell.Target.Alive = false;
				turn.AliveCount--;

				turn.AddLogMessage(new DeathEventLogMessage(
					playerSpell.Caster.Name,
					playerSpell.Target.Name,
					playerSpell.Spell.Name));
			}
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