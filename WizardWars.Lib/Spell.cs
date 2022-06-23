using WizardWars.Lib.Effects;

namespace WizardWars.Lib;

public class Spell
{
	public string Name { get; set; }
	public int ManaCost { get; set; }
	public int IQRequired { get; set; }

	public List<Effect> Effects { get; set; } = new();

	public void ApplyEffects(SpellPhase phase,Wizard caster, Wizard target)
	{
		foreach (var effect in Effects.Where(x => x.Phase == phase))
		{
			effect.Apply(caster, target);
		}
	}
}