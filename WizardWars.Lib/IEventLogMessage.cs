namespace WizardWars.Lib;

public interface IEventLogMessage
{
}

public record SpellCastLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;

public record DamageEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record HealEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record CounterEventLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;    //TODO: Counter doesnt counter stuff on same phase, but the message says so!

public record ManaGainEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;
