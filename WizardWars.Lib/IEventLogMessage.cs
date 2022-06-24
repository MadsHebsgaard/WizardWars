namespace WizardWars.Lib;

public interface IEventLogMessage
{
}

public record SpellCastLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;

public record DamageEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record HealEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record CounterEventLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;