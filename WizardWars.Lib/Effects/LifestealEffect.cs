namespace WizardWars.Lib.Effects;

public class LifeStealEffect : Effect
{
	public int LifeStealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.Health -= LifeStealAmount;
		playerSpell.Caster.Health += LifeStealAmount;


		turn.AddLogMessage(new LifeStealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			LifeStealAmount));
	}

}