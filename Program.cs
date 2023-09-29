using Heist.models;

Console.Clear();

List <IRobber> rolodex = new List<IRobber>()
{
    new Hacker
    {
        Name = "Syd Cyber",
        SkillLevel = 30,
        PercentageCut = 35
    },
    new Hacker
    {
        Name = "Emerick Dreadway",
        SkillLevel = 60,
        PercentageCut = 30
    },
    new Muscle
    {
        Name = "Beef Punchly",
        SkillLevel = 55,
        PercentageCut = 35
    },
    new Muscle
    {
        Name = "Strongarm Jones",
        SkillLevel = 40,
        PercentageCut = 25
    },
    new LockSpecialist
    {
        Name = "Montrose Pretty",
        SkillLevel = 58,
        PercentageCut = 35
    },
    new LockSpecialist
    {
        Name = "Jimmy Fingers",
        SkillLevel = 42,
        PercentageCut = 40
    }
};

List <IRobber> crew = new List<IRobber>();
int cutAvailable = 100;

Random r = new();
Bank bank = new()
{
    AlarmScore = r.Next(0, 100),
    VaultScore = r.Next(0, 100),
    SecurityGuardScore = r.Next(0, 100),
    CashOnHand = r.Next(50000, 1000000)
};

Dictionary<string, int> bankValues = new Dictionary<string, int>
{
    {"AlarmScore", bank.AlarmScore},
    {"VaultScore", bank.VaultScore},
    {"SecurityGuardScore", bank.SecurityGuardScore}
};
int maxValue = bankValues.Values.Max();
int minValue = bankValues.Values.Min();


void CharacterCreation() 
{
    Console.Clear();

    Console.WriteLine("Enter your criminal's name: ");
    string criminalName = Console.ReadLine();

    Console.Clear();
    Console.WriteLine("Specialties:");
    Console.WriteLine("1. Hacker (Disables alarms)");
    Console.WriteLine("2. Muscle (Disarms guards)");
    Console.WriteLine("3. Lock Specialist (Cracks vault)");

    int choice = 0;
    while (choice == 0)
    {
        Console.WriteLine("Choose your crook's specialty (1-3): ");
        try
        {
            int chosenSpecialty = int.Parse(Console.ReadLine());
            switch (chosenSpecialty)
            {
                case 1:
                    choice = 1;
                    break;
                
                case 2:
                    choice = 2;
                    break;

                case 3:
                    choice = 3;
                    break;
                
                default:
                    Console.WriteLine("Choose an option number...");
                    break;
            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("Choose an option number...");
        }
    }

    Console.Clear();

    int skillLevel = 0;
    while (skillLevel == 0)
    {
        Console.WriteLine("Enter a skill level from 1 to 100: ");
        try
        {
            int chosenSkillLevel = int.Parse(Console.ReadLine());
            if (chosenSkillLevel > 0 && chosenSkillLevel <= 100)
            {
                skillLevel = chosenSkillLevel;
            }
        }
        catch (System.Exception)
        {
            return;
        }
    }

    Console.Clear();

    int cut = 0;
    while (cut == 0)
    {
        Console.WriteLine("What is your rat's cut? (1-100)");
        try
        {
            int chosenCut = int.Parse(Console.ReadLine());
            if (chosenCut > 0 && chosenCut <= 100)
            {
                cut = chosenCut;
            }
        }
        catch (System.Exception)
        {
            return;
        }
    }

    switch (choice)
    {
        case 1:
            Hacker newHacker = new()
            {
                Name = criminalName,
                SkillLevel = skillLevel,
                PercentageCut = cut
            };
            rolodex.Add(newHacker);
            break;

        case 2:
            Muscle newMuscle = new()
            {
                Name = criminalName,
                SkillLevel = skillLevel,
                PercentageCut = cut
            };
            rolodex.Add(newMuscle);
            break;

        case 3:
            LockSpecialist newLockSpecialist = new()
            {
                Name = criminalName,
                SkillLevel = skillLevel,
                PercentageCut = cut
            };
            rolodex.Add(newLockSpecialist);
            break;

        default:
            return;
    }
}

void CrewSelect() 
{
    Console.WriteLine("Recon Report:");
    Console.WriteLine($"Most Secure: {bankValues.FirstOrDefault(bv => bv.Value == maxValue).Key}");
    Console.WriteLine($"Least Secure: {bankValues.FirstOrDefault(bv => bv.Value == minValue).Key}");
    Console.WriteLine("\nRolodex Report:\n");

    for (int i = 0; i < rolodex.Count; i++)
    {
        if ((cutAvailable - rolodex[i].PercentageCut) < 0)
        {
            continue;
        }
        Console.WriteLine($"{i + 1}.");
        Console.WriteLine($"Name: {rolodex[i].Name}");
        Console.WriteLine($"Specialty: {rolodex[i].GetType().Name}");
        Console.WriteLine($"Skill Level: {rolodex[i].SkillLevel}");
        Console.WriteLine($"Cut: {rolodex[i].PercentageCut}%\n");
    }

    
    Console.WriteLine("Enter the number of the crook you want in your crew: ");
    int choice = 0;
    while (choice == 0)
    {
        try
        {
            int chosenRobber = int.Parse(Console.ReadLine());
            crew.Add(rolodex[chosenRobber - 1]);
            cutAvailable -= rolodex[chosenRobber - 1].PercentageCut;
            rolodex.Remove(rolodex[chosenRobber - 1]);
            choice = 1;
        }
        catch (System.Exception)
        {
            Console.WriteLine("Select the number of the contact you want to hire...");
        }
    }
}

void PerformHeist() 
{
    foreach (IRobber robber in crew)
    {
        robber.PerformSkill(bank);
        Console.WriteLine("\n");
    }

    if (bank.IsSecure == false)
    {
        Console.WriteLine("Successful Heist!");
        PrintReport();
    }
    else
    {
        Console.WriteLine("Failed Heist. Your associates are arrested. You lose....");
    }
}

void PrintReport() 
{
    foreach (IRobber robber in crew)
    {
        var take = bank.CashOnHand * (robber.PercentageCut * 0.01M);
        bank.CashOnHand -= take;
        Console.WriteLine($"{robber.Name} gets ${take}\n");
    }

    Console.WriteLine($"You get ${bank.CashOnHand}");
}

string newScoundrel = null;
while (newScoundrel == null)
{
    Console.WriteLine("Criminal Rolodex:");
    foreach (IRobber robber in rolodex)
    {
        Console.WriteLine(robber.Name);
    }
    Console.WriteLine("Would you like to create a new scoundrel? (y/n)");
    string createNew = Console.ReadLine();

    try
    {
        switch (createNew)
        {
            case "y": case "Y":
            CharacterCreation();
            break;

            case "n": case "N":
            newScoundrel = "no";
            break;

            default:
            Console.Clear();
            break;

        }
    }
    catch (System.Exception)
    {
        Console.Clear();
    }
}

Console.Clear();

CrewSelect();

int heistReady = 0;
while (heistReady == 0)
{
    Console.Clear();
    Console.WriteLine("Current Crew: ");

    for (int i = 0; i < crew.Count; i++)
    {
        Console.WriteLine($"{i + 1}.");
        Console.WriteLine($"Name: {crew[i].Name}");
        Console.WriteLine($"Specialty: {crew[i].GetType().Name}");
        Console.WriteLine($"Skill Level: {crew[i].SkillLevel}");
        Console.WriteLine($"Cut: {crew[i].PercentageCut}%\n");
    }
    Console.WriteLine($"Cut available: {cutAvailable}%");
    Console.WriteLine("Hire another? (y/n)");
    string ready = Console.ReadLine();

    try
    {
        switch (ready)
        {
            case "y": case "Y":
            
            CrewSelect();
            break;

            case "n": case "N":
            heistReady = 1;
            break;

            default:
            break;
        }
    }
    catch (System.Exception)
    {
        break;
    }
}

PerformHeist();