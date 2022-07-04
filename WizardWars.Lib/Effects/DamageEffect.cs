namespace WizardWars.Lib.Effects;

public class DamageEffect : Effect
{
	public int DamageAmount { get; set; }
	public int TrueDamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
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

		playerSpell.Target.Health -= DamageTaken;

		turn.AddLogMessage(new DamageEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			DamageTaken));
	}
}