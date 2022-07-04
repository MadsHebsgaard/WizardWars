namespace WizardWars.Lib.Effects;

public class SelfDamageEffect : Effect
{

	public int DamageAmount { get; set; }
	public int TrueDamageAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		int BlockAmount = 0;
		if (playerSpell.Caster.Resistance != 0)
		{
			BlockAmount = Convert.ToInt32(DamageAmount * playerSpell.Caster.Resistance);
			turn.AddLogMessage(new BlockEventLogMessage(
				playerSpell.Caster.Name,
				playerSpell.Caster.Name,
				playerSpell.Spell.Name,
				BlockAmount));
		}
		int DamageTaken = TrueDamageAmount + DamageAmount - BlockAmount;
		playerSpell.Caster.Health -= DamageTaken;

		turn.AddLogMessage(new SelfDamageEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			DamageTaken));
	}
}