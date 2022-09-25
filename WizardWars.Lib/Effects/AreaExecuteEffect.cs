namespace WizardWars.Lib.Effects;

public class AreaExecuteEffect : Effect
{
	public int ExecuteAmount { get; set; }
	public bool ManaBack { get; set; } = false;
	public bool HealthBack { get; set; } = false;

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		foreach (var SpellTarget in turn.PlayerSpellList.Where(x => x.Caster.Alive))
		{
			if (SpellTarget.Caster.Health <= ExecuteAmount)
			{
				turn.AddLogMessage(new ExecuteEventLogMessage(
					playerSpell.Caster.Name,
					SpellTarget.Caster.Name,
					playerSpell.Spell.Name));
				
				if (ManaBack)
				{
					int TrueManaGain = Math.Min(SpellTarget.Caster.Mana,
						playerSpell.Caster.MaxMana - playerSpell.Caster.Mana);
					
					playerSpell.Caster.Mana += TrueManaGain;
					SpellTarget.Caster.Mana = 0;

					turn.AddLogMessage(new SelfRestoreManaEventLogMessage(
						playerSpell.Caster.Name,
						playerSpell.Spell.Name,
						TrueManaGain));
				}

				if (HealthBack)
				{
					int TrueHealthGain = Math.Min(SpellTarget.Caster.Health,
						playerSpell.Caster.MaxHealth - playerSpell.Caster.Health);

					playerSpell.Caster.Health += TrueHealthGain;
					
					turn.AddLogMessage(new SelfHealEventLogMessage(
						playerSpell.Caster.Name,
						playerSpell.Spell.Name,
						TrueHealthGain));
				}
				SpellTarget.Caster.Health = 0;
				SpellTarget.Caster.Alive = false;
			}
		}
	}
}