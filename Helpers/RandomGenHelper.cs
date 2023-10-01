public static class RandomGenHelper
{
    private static int Seed()
    {
        var dateTimeForSeed = DateTime.Now;
        return (int)dateTimeForSeed.Ticks;
    }

    public static int GenerateRandomNumber()
    {
        var random = new Random(Seed());
        return random.Next();
    }
}