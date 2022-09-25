namespace WizardWars.Lib;

public class Wizard
{
	public string Name { get; }
	public int Health { get; set; } = 100;
	public int Mana { get; set; } = 100;
	public double LVL { get; set; } = 1;
	public int HealthRegen { get; set; } = 0;
	public int ManaRegen { get; set; } = 15;
	public double LVLRegen { get; set; } = 0;
	public double Resistance { get; set; } = 0;
	public static int LVLHeal {get; } = 5 ;
	public int WinCount { get; set; } = 0;
	public int MaxHealth { get; set; } = 100;
	public int MaxMana { get; set; } = 100;
	public static int MaxLVL { get; } = 10;
	public bool Alive { get; set; } = true;
	public List<Spell> Spellbook { get; set; }

	public double DamageMultiplier { get; set; } = 1;

	public Wizard(string name)
	{
		Name = name;
	}
}