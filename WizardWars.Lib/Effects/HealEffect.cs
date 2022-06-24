namespace WizardWars.Lib.Effects;

public class HealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.Health += HealAmount;
		
		turn.AddLogMessage(new HealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			HealAmount));
	}
}