namespace StoryTeller;

public static class Helper
{
    public static Random random = new Random();
    
    public static string GetRandom(List<string> stringList)
    {
        var index = random.Next(stringList.Count);
        return stringList[index];
    }
}