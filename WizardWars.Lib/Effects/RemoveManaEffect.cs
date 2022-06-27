namespace WizardWars.Lib.Effects;

public class RemoveManaEffect : Effect
{
	public int RemoveManaAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.Mana -= RemoveManaAmount;

		turn.AddLogMessage(new RemoveManaEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			RemoveManaAmount));
	}
}