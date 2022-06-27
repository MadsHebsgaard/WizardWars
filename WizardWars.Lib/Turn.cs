namespace WizardWars.Lib;

public class Turn
{
	public SpellTarget FirstPlayerSpell { get; }
	public SpellTarget SecondPlayerSpell { get; }

	public Turn(SpellTarget firstPlayerSpell, SpellTarget secondPlayerSpell)
	{
		FirstPlayerSpell = firstPlayerSpell;
		SecondPlayerSpell = secondPlayerSpell;
	}

	private readonly List<IEventLogMessage> _eventLog = new List<IEventLogMessage>();
	public IReadOnlyList<IEventLogMessage> EventLog => _eventLog;

	public void AddLogMessage(IEventLogMessage message)
	{
		_eventLog.Add(message);
	}

	public void Execute()
	{
		AddLogMessage(new SpellCastLogMessage(FirstPlayerSpell.Caster.Name, FirstPlayerSpell.Target.Name,
			FirstPlayerSpell.Spell.Name));
		AddLogMessage(new SpellCastLogMessage(SecondPlayerSpell.Caster.Name, SecondPlayerSpell.Target.Name,
			SecondPlayerSpell.Spell.Name));

		foreach (var phase in Enum.GetValues<SpellPhase>().Skip(1))
		{
			if(FirstPlayerSpell.Continue && SecondPlayerSpell.Continue)
            {
				FirstPlayerSpell.Spell.ApplyEffects(phase, FirstPlayerSpell, this);
				SecondPlayerSpell.Spell.ApplyEffects(phase, SecondPlayerSpell, this);
			}
			if (FirstPlayerSpell.Caster.Health <= 0 || SecondPlayerSpell.Caster.Health <= 0)
			{
				return;
			}
		}
	}
}