namespace WizardWars.Lib.Effects;

public class DamageMultiplierEffect : Effect
{
	public double DamageMultiplierAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Caster.DamageMultiplier *= DamageMultiplierAmount;
		turn.AddLogMessage(new DamageMultiplierEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			playerSpell.Caster.DamageMultiplier-playerSpell.Caster.DamageMultiplier/DamageMultiplierAmount,
			playerSpell.Caster.DamageMultiplier-1));
	}
}
