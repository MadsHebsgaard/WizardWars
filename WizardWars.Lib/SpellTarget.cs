namespace WizardWars.Lib;

public record SpellTarget(Wizard Caster, Spell Spell, Wizard Target)
{
	public bool Continue { get; set; } = false;
}
