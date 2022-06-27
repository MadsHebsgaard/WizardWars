namespace WizardWars.Lib;

public interface IEventLogMessage
{
}

public record SpellCastLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;

public record DamageEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record HealEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record CounterEventLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;    //TODO: Counter doesnt counter stuff on same phase, but the message says so! //TODO: wizard1 counters own counterspell if both uses it.

public record ManaGainEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record LifeStealEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record ManaStealEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record SelfDamageEventLogMessage(string Source, string SpellName, int Amount) : IEventLogMessage;

public record AreaHealEventLogMessage(string Source, string SpellName, int Amount) : IEventLogMessage;

public record SelfHealEventLogMessage(string Source, string SpellName, int Amount) : IEventLogMessage;

public record RemoveManaEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record SelfRestoreManaEventLogMessage(string Source, string SpellName, int Amount) : IEventLogMessage;

public record LVLEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record SelfLVLEventLogMessage(string Source, string SpellName, int Amount) : IEventLogMessage;



