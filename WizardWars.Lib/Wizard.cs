namespace WizardWars.Lib;

public class Wizard
{
	public string Name { get; }
	public int Health { get; set; } = 100;
	public int Mana { get; set; } = 100; //3, testing
	public double LVL { get; set; } = 1; //1, testing
	public int HealthRegen { get; set; } = 0;
	public int ManaRegen { get; set; } = 25; //2-3, testing
	public double LVLRegen { get; set; } = 0;



	public Wizard(string name)
	{
		Name = name;
	}

	/*
	public Wizard GetTarget(Spell spell, IUserInterface userInterface, Wizard self, Wizard enemy)
	{
		return spell.TargetType switch
		{
			TargetType.Select => userInterface.UserPicksTarget() == Target.Self ? self : enemy,
			TargetType.SelfOnly => self,
			TargetType.EnemyOnly => enemy
		};
	}
	*/

}