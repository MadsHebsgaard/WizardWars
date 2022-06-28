namespace WizardWars.Lib;

public class SpellTarget
{
	public Wizard Caster { get; set; }
    public Spell Spell { get; set; }
    public Wizard Target { get; set; }
    public bool Continue { get; set; } = true;

    public SpellTarget(Wizard caster, Spell spell, Wizard target)
    {
        Caster = caster;
        Spell = spell;
        Target = target;
    }
}
