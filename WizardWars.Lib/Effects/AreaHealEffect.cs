namespace WizardWars.Lib.Effects;

public class AreaHealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		var enemySpellCast = playerSpell == turn.FirstPlayerSpell ? turn.SecondPlayerSpell : turn.FirstPlayerSpell;

		int HealthHealed1 = Math.Min(HealAmount, playerSpell.Caster.MaxHealth - playerSpell.Caster.Health);
		int HealthHealed2 = Math.Min(HealAmount, enemySpellCast.Caster.MaxHealth - enemySpellCast.Caster.Health);
		playerSpell.Caster.Health += HealthHealed1;
		enemySpellCast.Caster.Health += HealthHealed2;

		turn.AddLogMessage(new AreaHealEventLogMessage(
			playerSpell.Caster.Name,
			enemySpellCast.Caster.Name,
			playerSpell.Spell.Name,
			HealthHealed1,
			HealthHealed2));
	}
}