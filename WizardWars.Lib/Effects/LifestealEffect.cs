namespace WizardWars.Lib.Effects;

public class LifeStealEffect : Effect
{
	public int DamageAmount { get; set; }
	public int TrueDamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		if (playerSpell.Target.Alive)
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
		
			DamageTaken = Math.Min(DamageTaken, playerSpell.Target.Health);
		int HealthHealed = Math.Min(DamageTaken, playerSpell.Caster.MaxHealth-playerSpell.Caster.Health);

		playerSpell.Target.Health -= DamageTaken;
		playerSpell.Caster.Health += HealthHealed;

		turn.AddLogMessage(new LifeStealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			DamageTaken,
			HealthHealed));
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