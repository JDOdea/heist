namespace Heist.models;

public class Bank
{
    public decimal CashOnHand { get; set; }
    public int AlarmScore { get; set; }
    public int VaultScore { get; set; }
    public int SecurityGuardScore { get; set; }
    public bool IsSecure
    {
        get
        {
            bool secure = false;

            int totalScore = AlarmScore + VaultScore + SecurityGuardScore;

            if (totalScore > 0)
            {
                secure = true;
            }

            return secure;
        }
    }
}