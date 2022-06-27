﻿namespace WizardWars.Lib.Effects;

public class LVLEffect : Effect
{
	public int LVLAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		playerSpell.Target.LVL += LVLAmount;
		
		turn.AddLogMessage(new LVLEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Target.Name,
			playerSpell.Spell.Name,
			LVLAmount));
	}
}