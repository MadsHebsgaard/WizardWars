namespace WizardWars.Lib;

public class Wizard
{
	public string Name { get; }
	public int Mana { get; set; } = 3;
	public int Knowledge { get; set; } = 2;
	public int Health { get; set; } = 100;
	public int ManaRegen { get; set; } = 2;
	public int HealthRegen { get; set; } = 0;



	public Wizard(string name)
	{
		Name = name;
	}
}