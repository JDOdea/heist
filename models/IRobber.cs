namespace Heist.models;

public interface IRobber
{
    string Name { get; }
    int SkillLevel { get; }
    int PercentageCut { get; }

    public void PerformSkill (Bank bank)
    {
        
    }
}