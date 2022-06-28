namespace WizardWars.Lib;

public class Turn
{
	public SpellTarget FirstPlayerSpell { get; set; }
	public SpellTarget SecondPlayerSpell { get; set; }

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
		AddLogMessage(new SpellCastLogMessage(FirstPlayerSpell.Caster.Name, FirstPlayerSpell.Target.Name, FirstPlayerSpell.Spell.Name, FirstPlayerSpell.Spell.ManaCost, FirstPlayerSpell.Spell.HealthCost));
		AddLogMessage(new SpellCastLogMessage(SecondPlayerSpell.Caster.Name, SecondPlayerSpell.Target.Name, SecondPlayerSpell.Spell.Name, SecondPlayerSpell.Spell.ManaCost, SecondPlayerSpell.Spell.HealthCost));

		foreach (var phase in Enum.GetValues<SpellPhase>().Skip(1))
		{
			//CounterPlayerSpell(FirstPlayerSpell, SecondPlayerSpell); //TODO: This function
			if (!SecondPlayerSpell.Continue)
			{
				FirstPlayerSpell = new SpellTarget(FirstPlayerSpell.Caster, new Spell(), FirstPlayerSpell.Target);
			}
			if (!FirstPlayerSpell.Continue)
			{
				SecondPlayerSpell = new SpellTarget(SecondPlayerSpell.Caster, new Spell(), SecondPlayerSpell.Target);
			}

			FirstPlayerSpell.Spell.ApplyEffects(phase, FirstPlayerSpell, this);
			SecondPlayerSpell.Spell.ApplyEffects(phase, SecondPlayerSpell, this);

			if (FirstPlayerSpell.Caster.Health <= 0 || SecondPlayerSpell.Caster.Health <= 0)
			{
				return;
			}
		}
	}
}