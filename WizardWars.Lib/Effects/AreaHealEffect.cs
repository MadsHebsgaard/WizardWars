namespace WizardWars.Lib.Effects;

public class AreaHealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		foreach (var PlayerSpell in turn.PlayerSpellList)
        {
			PlayerSpell.Caster.Health += HealAmount;
		}

		turn.AddLogMessage(new AreaHealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			HealAmount));
	}
}