namespace WizardWars.Lib.Effects;

public class LifeStealEffect : Effect
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
		int HealthHealed = Math.Min(DamageTaken, playerSpell.Caster.MaxHealth-playerSpell.Caster.Health);

		playerSpell.Target.Health -= DamageTaken;
		playerSpell.Caster.Health += HealthHealed;

		turn.AddLogMessage(new LifeStealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			DamageTaken,
			HealthHealed));








		/*
		int BlockAmount = 0;
		int TrueLifeStealAmount = LifeStealAmount;
		if (playerSpell.Target.Resistance > 0)
		{
			BlockAmount = Convert.ToInt32(LifeStealAmount * playerSpell.Target.Resistance);
			var enemySpellCast = playerSpell == turn.FirstPlayerSpell ? turn.SecondPlayerSpell : turn.FirstPlayerSpell;
			turn.AddLogMessage(new BlockEventLogMessage(
				enemySpellCast.Caster.Name,
				playerSpell.Caster.Name,
				playerSpell.Spell.Name,
				BlockAmount));
		}
		TrueLifeStealAmount -= BlockAmount;
		playerSpell.Target.Health -= TrueLifeStealAmount;
		playerSpell.Caster.Health += TrueLifeStealAmount;

		turn.AddLogMessage(new LifeStealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			TrueLifeStealAmount));
		*/
	}
}