namespace WizardWars.Lib.Effects;

public class KnowledgeEffect : Effect
{
	public int KnowledgeAmount { get; set; }

	public override void Apply(Wizard caster, Wizard target)
	{
		target.Knowledge += KnowledgeAmount;
	}
}