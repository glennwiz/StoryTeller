public class Objects
{
    public List<string> MagicalItems { get; set; }
    public List<string> EverydayItems { get; set; }
    public List<string> Weapons { get; set; }
    public List<string> Artifacts { get; set; }

    public List<string> GetAllObjects()
    {
        return new List<string> { GetRandom(MagicalItems), GetRandom(EverydayItems), GetRandom(Weapons), GetRandom(Artifacts) };
    }

    private string GetRandom(List<string> stringList)
    {
        Random random = new Random();
        int index = random.Next(stringList.Count);
        return stringList[index];
    }
}
