namespace WizardWars.Lib;

public interface IEventLogMessage
{
}

public record SpellCastLogMessage (string Source, string Target, string SpellName, int ManaCost, int HealthCost) : IEventLogMessage;

public record DamageEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record HealEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record CounterEventLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;

public record FailCounterEventLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;

public record RedirectEventLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;

public record FailRedirectEventLogMessage(string Source, string Target, string SpellName) : IEventLogMessage;

public record BlockEventLogMessage(string Source, string Target, string SpellName, double Amount) : IEventLogMessage;

public record ManaGainEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record LifeStealEventLogMessage(string Source, string Target, string SpellName, int DamageDealt, int HealthHealed) : IEventLogMessage;

public record ManaStealEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record SelfDamageEventLogMessage(string Source, string SpellName, int Amount) : IEventLogMessage;

public record AreaRestoreManaEventLogMessage(string Source, string Player2, string SpellName, int Amount1, int Amount2) : IEventLogMessage;

public record AreaHealEventLogMessage(string Source, string Player2, string SpellName, int Amount1, int Amount2) : IEventLogMessage;

public record SelfHealEventLogMessage(string Source, string SpellName, int Amount) : IEventLogMessage;

public record RemoveManaEventLogMessage(string Source, string Target, string SpellName, int Amount) : IEventLogMessage;

public record SelfRestoreManaEventLogMessage(string Source, string SpellName, int Amount) : IEventLogMessage;

public record LVLEventLogMessage(string Source, string Target, string SpellName, double Amount) : IEventLogMessage;

public record SelfLVLEventLogMessage(string Source, string SpellName, double Amount) : IEventLogMessage;

public record SelfResistanceEventLogMessage(string Source, string SpellName, double Amount) : IEventLogMessage;



