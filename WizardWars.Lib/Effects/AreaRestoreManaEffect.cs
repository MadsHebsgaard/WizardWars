namespace WizardWars.Lib.Effects;

public class AreaRestoreManaEffect : Effect
{
	public int RestoreManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		foreach (var PlayerSpell in turn.PlayerSpellList.Where(x => x.Caster.Alive).ToList())
		{
			PlayerSpell.Caster.Mana += RestoreManaAmount;
		}
		turn.AddLogMessage(new AreaRestoreManaEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			RestoreManaAmount));
	}
}