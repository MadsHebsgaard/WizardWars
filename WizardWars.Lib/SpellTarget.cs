namespace WizardWars.Lib;

public record SpellTarget(Wizard Caster, Spell Spell, Wizard Target)
{
	public bool Continue { get; set; } = true;  //Befor it was true until set to false. (" ... continue {get; set;} = false;")
}
