namespace WizardWars.Lib.Effects;

public class RedirectEffect : Effect
{
    // TODO: needs work!

    public override void Apply(SpellTarget playerSpell, Turn turn)
    {
        bool redirect = false;

        foreach (var SpellTarget in turn.PlayerSpellList)
        {
            if (SpellTarget.Target == playerSpell.Target && playerSpell.Caster != SpellTarget.Caster)
            {
                if (SpellTarget.Spell.TriggerPhase > playerSpell.Spell.TriggerPhase && SpellTarget.Spell.TriggerPhase <= playerSpell.Spell.StopPhase
                || SpellTarget.Spell.TriggerPhase == playerSpell.Spell.TriggerPhase && turn.PlayerSpellList.IndexOf(playerSpell) < turn.PlayerSpellList.IndexOf(SpellTarget))
                {
                    //if (SpellTarget.Spell.TargetType!=AOE)

                    redirect = true;
                    SpellTarget.Target = SpellTarget.Caster;

                    turn.AddLogMessage(new RedirectEventLogMessage(
                        playerSpell.Caster.Name,
                        SpellTarget.Caster.Name,
                        SpellTarget.Spell.Name));
                }
            }
        }
        if(!redirect)
        {
            turn.AddLogMessage(new FailRedirectEventLogMessage(
                playerSpell.Caster.Name,
                playerSpell.Target.Name));
        }
    }
}

