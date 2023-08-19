using StoryTeller;

public class Actions
{
    public List<string> CombatMoves { get; set; }
    public List<string> TravelMethods { get; set; }
    public List<string> Interactions { get; set; }
    public List<string> PhysicalActions { get; set; }
    public List<string> MentalActions { get; set; }
    public List<string> EmotionalActions { get; set; }
    public List<string> DailyActivities { get; set; }

    public List<string> GetRandomActions()
    {
        //use the helper method to get a random item from each list
        return new List<string> { Helper.GetRandom(CombatMoves), Helper.GetRandom(TravelMethods), Helper.GetRandom(Interactions), Helper.GetRandom(PhysicalActions), Helper.GetRandom(MentalActions), Helper.GetRandom(EmotionalActions), Helper.GetRandom(DailyActivities) };
    }
}

