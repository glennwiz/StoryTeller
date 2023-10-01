using StoryTeller;

public class SettingsRoot
{
    public Settings Settings { get; set; }
}

public class Settings
{
    public UrbanLocations UrbanLocations { get; set; }
    public RuralLocations RuralLocations { get; set; }
    public IndoorSettings IndoorSettings { get; set; }
    public NaturalLandmarks NaturalLandmarks { get; set; }
    public FantasyRealms FantasyRealms { get; set; }

    public List<string> GetRandomLocation()
    {
        //use the helper method to get a random item from each list
        return new List<string> { Helper.GetRandom(UrbanLocations.Cities), Helper.GetRandom(UrbanLocations.Towns), Helper.GetRandom(RuralLocations.Villages), Helper.GetRandom(RuralLocations.Farms), Helper.GetRandom(RuralLocations.Forests), Helper.GetRandom(IndoorSettings.Houses), Helper.GetRandom(IndoorSettings.Castles), Helper.GetRandom(IndoorSettings.Inns), Helper.GetRandom(NaturalLandmarks.Mountains), Helper.GetRandom(NaturalLandmarks.Rivers), Helper.GetRandom(NaturalLandmarks.Lakes), Helper.GetRandom(FantasyRealms.MagicalLands), Helper.GetRandom(FantasyRealms.OtherDimensions) };
    }
}

public class UrbanLocations
{
    public List<string> Cities { get; set; }
    public List<string> Towns { get; set; }
}

public class RuralLocations
{
    public List<string> Villages { get; set; }
    public List<string> Farms { get; set; }
    public List<string> Forests { get; set; }
}

public class IndoorSettings
{
    public List<string> Houses { get; set; }
    public List<string> Castles { get; set; }
    public List<string> Inns { get; set; }
}

public class NaturalLandmarks
{
    public List<string> Mountains { get; set; }
    public List<string> Rivers { get; set; }
    public List<string> Lakes { get; set; }
}

public class FantasyRealms
{
    public List<string> MagicalLands { get; set; }
    public List<string> OtherDimensions { get; set; }
}
