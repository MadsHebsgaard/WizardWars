﻿namespace WizardWars.Lib.Effects;

public class AreaHealEffect : Effect
{
	public int HealAmount { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		turn.SecondPlayerSpell.Caster.Health += HealAmount;
		turn.FirstPlayerSpell.Caster.Health += HealAmount;

		turn.AddLogMessage(new AreaHealEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			HealAmount));
	}
}