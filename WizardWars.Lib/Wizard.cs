namespace WizardWars.Lib;

public class Wizard
{
	public string Name { get; }
	public int Mana { get; set; }
	public int Health { get; set; }

	public Wizard(string name)
	{
		Name = name;
	}
}