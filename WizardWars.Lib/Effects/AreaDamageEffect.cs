namespace WizardWars.Lib.Effects;

public class AreaDamageEffect : Effect
{
	public int DamageAmount { get; set; }
	public int TrueDamageAmount { get; set; }
	public bool WithSelf { get; set;} = true;

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		turn.AddLogMessage(new AreaDamageEventLogMessage(
			playerSpell.Caster.Name,
			playerSpell.Spell.Name,
			WithSelf,
			DamageAmount + TrueDamageAmount));

		foreach (var PlayerSpell in turn.PlayerSpellList.Where(x => x.Caster.Alive).ToList())
        {
			if (WithSelf || PlayerSpell.Caster != playerSpell.Caster)
			{
				int BlockAmount = 0;
				if (PlayerSpell.Caster.Resistance != 0 && DamageAmount>0)
				{
					BlockAmount = Convert.ToInt32(DamageAmount * PlayerSpell.Caster.Resistance);
					turn.AddLogMessage(new BlockEventLogMessage(
						PlayerSpell.Caster.Name,
						playerSpell.Caster.Name,
						playerSpell.Spell.Name,
						BlockAmount));
				}
				int DamageTaken = Convert.ToInt32((TrueDamageAmount + DamageAmount - BlockAmount) * PlayerSpell.Caster.DamageMultiplier);
				PlayerSpell.Caster.Health -= DamageTaken;
				if (PlayerSpell.Caster.Health <= 0) //Dead wizard check
				{
					PlayerSpell.Caster.Health = 0;
					PlayerSpell.Caster.Alive = false;
					turn.AliveCount--;

					turn.AddLogMessage(new DeathEventLogMessage(
						playerSpell.Caster.Name,
						PlayerSpell.Caster.Name,
						playerSpell.Spell.Name));
				}
			}
		}
	}
}