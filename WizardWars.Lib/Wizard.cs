namespace WizardWars.Lib;

public class Wizard
{
	public string Name { get; }
	public int Health { get; set; } = 100;
	public int Mana { get; set; } = 100; //3, testing
	public double LVL { get; set; } = 1; //1, testing
	public int HealthRegen { get; set; } = 0;
	public int ManaRegen { get; set; } = 15; //2-3, testing
	public double LVLRegen { get; set; } = 0;

	public int WinCount { get; set; } = 0;

	public Wizard(string name)
	{
		Name = name;
	}
}