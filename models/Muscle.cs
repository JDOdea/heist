namespace Heist.models;

public class Muscle : IRobber
{
    public string Name { get; set; }
    public int SkillLevel { get; set; }
    public int PercentageCut { get; set; }

    public void PerformSkill(Bank bank)
    {
        Console.WriteLine($"{Name} is taking care of the guards. Decreased security {SkillLevel} points.");
        bank.SecurityGuardScore -= SkillLevel;

        if (bank.SecurityGuardScore < 1)
        {
            Console.WriteLine($"{Name} has incapacitated the guards!");
        }
    }
}