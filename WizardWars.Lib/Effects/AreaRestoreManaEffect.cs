namespace WizardWars.Lib.Effects;

public class AreaRestoreManaEffect : Effect
{
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		var enemySpellCast = playerSpell == turn.FirstPlayerSpell ? turn.SecondPlayerSpell : turn.FirstPlayerSpell;

		int ManaRestored1 = Math.Min(RestoreManaAmount, playerSpell.Caster.MaxMana - playerSpell.Caster.Mana);
		int ManaRestored2 = Math.Min(RestoreManaAmount, enemySpellCast.Caster.MaxMana - enemySpellCast.Caster.Mana);
		playerSpell.Caster.Mana += ManaRestored1;
		enemySpellCast.Caster.Mana += ManaRestored2;

		turn.AddLogMessage(new AreaRestoreManaEventLogMessage(
			playerSpell.Caster.Name,
			enemySpellCast.Caster.Name,
			playerSpell.Spell.Name,
			ManaRestored1,
			ManaRestored2));
	}
}