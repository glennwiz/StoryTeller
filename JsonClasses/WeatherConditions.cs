public class WeatherConditions
{
    public SunnyDays SunnyDays { get; set; }
    public RainyDays RainyDays { get; set; }
    public SnowyDays SnowyDays { get; set; }
    public WindyDays WindyDays { get; set; }
}

public class SunnyDays
{
    public string Description { get; set; }
    public List<string> Conditions { get; set; }
}

public class RainyDays
{
    public string Description { get; set; }
    public List<string> Conditions { get; set; }
}

public class SnowyDays
{
    public string Description { get; set; }
    public List<string> Conditions { get; set; }
}

public class WindyDays
{
    public string Description { get; set; }
    public List<string> Conditions { get; set; }
}