using WizardWars.Lib.Effects;
namespace WizardWars.Lib;

public class Spell
{
	public string Name { get; set; } = "Nothing";
	public int ManaCost { get; set; }
	public int HealthCost { get; set; }
	public int LVLRequired { get; set; }
	public SpellPhase TriggerPhase { get; set; } = SpellPhase.One;
	public SpellPhase StopPhase { get; set; } = SpellPhase.Five;
	public TargetType TargetType { get; set; }
	public SpellType SpellType { get; set; }


	public List<Effect> Effects { get; set; } = new();

	public void ApplyEffects(SpellPhase phase, SpellTarget playerSpell, Turn turn)
	{
		foreach (var effect in Effects.Where(x => x.Phase == phase))
		{
			effect.Apply(playerSpell, turn);
		}
	}
	public readonly static Spell Nothing = new Spell();
}