namespace WizardWars.Lib;

public class Wizard
{
	public string Name { get; }
	public int Health { get; set; } = 100;
	public int Mana { get; set; } = 100; //3, testing
	public int IQ { get; set; } = 200; //100, testing
	public int HealthRegen { get; set; } = 0;
	public int ManaRegen { get; set; } = 0; //2-3, testing
	public int IQRegen { get; set; } = 0;



	public Wizard(string name)
	{
		Name = name;
	}
}