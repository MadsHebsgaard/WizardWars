namespace WizardWars.Lib.Effects;

public class RedirectEffect : Effect
{
    // TODO: needs work!

    public override void Apply(SpellTarget playerSpell, Turn turn)
    {
        // TODO: rework trickery
        var enemySpellCast = playerSpell == turn.FirstPlayerSpell ? turn.SecondPlayerSpell : turn.FirstPlayerSpell;

        if (enemySpellCast.Spell.TriggerPhase <= playerSpell.Spell.TriggerPhase || enemySpellCast.Spell.TriggerPhase > playerSpell.Spell.StopPhase)
        {
            turn.AddLogMessage(new FailRedirectEventLogMessage(
            playerSpell.Caster.Name,
            playerSpell.Target.Name,
            enemySpellCast.Spell.Name));
            return;
        }
        if (playerSpell.Target == enemySpellCast.Target)
        {
            if (enemySpellCast.Target == enemySpellCast.Caster) { enemySpellCast.Target = playerSpell.Caster; }
            else { enemySpellCast.Target = playerSpell.Caster; }

            turn.AddLogMessage(new RedirectEventLogMessage(
                playerSpell.Caster.Name,
                playerSpell.Target.Name,
                enemySpellCast.Spell.Name));
        }
    }
}