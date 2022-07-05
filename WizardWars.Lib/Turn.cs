namespace WizardWars.Lib;

public class Turn
{
	public List <SpellTarget> PlayerSpellList { get; set; }

	public Turn(List <SpellTarget> playerSpellList)
	{
		PlayerSpellList = playerSpellList;
	}

	private readonly List<IEventLogMessage> _eventLog = new List<IEventLogMessage>();
	public IReadOnlyList<IEventLogMessage> EventLog => _eventLog;

	public void AddLogMessage(IEventLogMessage message)
	{
		_eventLog.Add(message);
	}

	public void Execute()
	{
		foreach(var SpellTarget in PlayerSpellList)
        {
			AddLogMessage(new SpellCastLogMessage(SpellTarget.Caster.Name, SpellTarget.Target.Name, SpellTarget.Spell.Name, SpellTarget.Spell.ManaCost, SpellTarget.Spell.HealthCost));
			SpellTarget.Caster.Mana -= SpellTarget.Spell.ManaCost;
			SpellTarget.Caster.Health -= SpellTarget.Spell.HealthCost;
		}

		foreach (var phase in Enum.GetValues<SpellPhase>()/*.Skip(1)*/)
		{
			
			foreach(var SpellTarget in PlayerSpellList)
            {
				SpellTarget.Spell.ApplyEffects(phase, SpellTarget, this); //Wizards that die should be removed from Loop and list.
			}
		}
	}
}