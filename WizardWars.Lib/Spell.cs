using WizardWars.Lib.Effects;

namespace WizardWars.Lib;

public class Spell
{
	public string Name { get; set; } //TODO nullable
	public int ManaCost { get; set; }
	public int LVLRequired { get; set; }

	public List<Effect> Effects { get; set; } = new();

	public void ApplyEffects(SpellPhase phase, SpellTarget playerSpell, Turn turn)
	{
		foreach (var effect in Effects.Where(x => x.Phase == phase))
		{
			effect.Apply(playerSpell, turn);
		}
	}
}