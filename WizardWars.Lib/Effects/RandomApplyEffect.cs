/*

namespace WizardWars.Lib.Effects;

public class RandomApplyEffect : Effect
{
	public int DamageAmount { get; set; }
	public int TrueDamageAmount { get; set; }
	public string SpellType { get; set; }

	public override void Apply(SpellTarget playerSpell, Turn turn)
	{
		Spell spell = playerSpell.Spell;
		spell.type = SpellType;


		Random rnd = new Random();
		int TargetN;

		List<SpellTarget> SpellTargetList = turn.PlayerSpellList.Where(x => x.Caster.Alive).ToList();

		for (int i = 0; i < 2*turn.AliveCount; i++)
        {
			TargetN = rnd.Next(0, turn.AliveCount-1);
			SpellTarget RndPlayerSpell = new SpellTarget(playerSpell.Caster, spell, SpellTargetList[TargetN].Caster);
			Apply(playerSpell, turn);
		}
	}
}

*/