namespace StoryTeller;

public static class Helper
{
    public static Random random = new Random();
    
    public static string GetRandom(List<string> stringList)
    {
        int index = random.Next(stringList.Count);
        return stringList[index];
    }
}