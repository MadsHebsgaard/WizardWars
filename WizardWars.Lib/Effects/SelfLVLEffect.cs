namespace WizardWars.Lib.Effects;

public class SelfLVLEffect : Effect
{
	public int LVLAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Caster.LVL += LVLAmount;
		
		turn.AddLogMessage(new SelfLVLEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			LVLAmount));
	}

}