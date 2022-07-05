namespace WizardWars.Lib;

public class Turn
{
	public List <SpellTarget> PlayerSpellList { get; set; }
	public int AliveCount { get; set; }

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
		AliveCount = 0;
		foreach(var SpellTarget in PlayerSpellList)
        {
			AddLogMessage(new SpellCastLogMessage(SpellTarget.Caster.Name, SpellTarget.Target.Name, SpellTarget.Spell.Name, SpellTarget.Spell.ManaCost, SpellTarget.Spell.HealthCost));
			SpellTarget.Caster.Mana -= SpellTarget.Spell.ManaCost;
			SpellTarget.Caster.Health -= SpellTarget.Spell.HealthCost;
			AliveCount++;
		}
		AddLogMessage(new SpaceLogMessage());
		foreach (var phase in Enum.GetValues<SpellPhase>()/*.Skip(1)*/)
		{
			foreach(var SpellTarget in PlayerSpellList)
            {
				if(AliveCount >= 2)
                {
					if (SpellTarget.Caster.Alive) { SpellTarget.Spell.ApplyEffects(phase, SpellTarget, this); }
				}
                else
                {
					break;
				}
			}
		}
	}
}