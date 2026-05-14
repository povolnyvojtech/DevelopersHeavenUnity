public class Job
{
    public string JobType;
    public int JobTime;
    public int JobMoney;
    public int JobXp;

    public Job(string type, int time, int money, int xp)
    {
        JobType = type;
        JobTime = time;
        JobMoney = money;
        JobXp = xp;
    }

    public override string ToString()
    {
        return "Time: " + JobTime + " Money: " + JobMoney + " XP: " + JobXp;
    }
}