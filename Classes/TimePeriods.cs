namespace StoryTeller.Classes
{
    public class TimePeriods
    {
        public Past Past { get; set; }
        public Present Present { get; set; }
        public Future Future { get; set; }
        public Timeless Timeless { get; set; }

        public List<string> GetAllTimePeriods()
        { 
            //return the names of all the properties in this class as a list of strings
            var returnList = new List<string>();
            foreach (var property in this.GetType().GetProperties())
            {
                returnList.Add(property.Name);
            }

            return returnList;
        }
    }

    public class Past
    {
        public Ancient Ancient { get; set; }
        public Medieval Medieval { get; set; }
        public Renaissance Renaissance { get; set; }
        public Modern Modern { get; set; }
    }

    public class Ancient
    {
        public List<string> Civilizations { get; set; }
        public List<string> Events { get; set; }
    }

    public class Medieval
    {
        public List<string> Regions { get; set; }
        public List<string> Events { get; set; }
    }

    public class Renaissance
    {
        public List<string> Regions { get; set; }
        public List<string> Events { get; set; }
    }

    public class Modern
    {
        public List<string> Eras { get; set; }
        public List<string> Events { get; set; }
    }

    public class Present
    {
        public List<string> Decades { get; set; }
        public List<string> Events { get; set; }
    }

    public class Future
    {
        public NearFuture NearFuture { get; set; }
        public DistantFuture DistantFuture { get; set; }
    }

    public class NearFuture
    {
        public List<string> Predictions { get; set; }
        public List<string> TechAdvancements { get; set; }
    }

    public class DistantFuture
    {
        public List<string> Scenarios { get; set; }
        public List<string> TechAdvancements { get; set; }
    }

    public class Timeless
    {
        public List<string> Settings { get; set; }
        public List<string> Events { get; set; }
    }
}
