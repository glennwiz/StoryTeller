using StoryTeller;

public class PlotTwists
{
    public List<string> Revelations { get; set; }
    public List<string> Betrayals { get; set; }
    public List<string> Mysteries { get; set; }
    public List<string> Challenges { get; set; }

    public List<string> GetAllPlotTwists()
    {
        //use the helper method to get a random item from each list
        return new List<string> { Helper.GetRandom(Revelations), Helper.GetRandom(Betrayals), Helper.GetRandom(Mysteries), Helper.GetRandom(Challenges) };
    }
}
