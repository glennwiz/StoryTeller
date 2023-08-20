using StoryTeller;

namespace StoryTeller.Classes
{
    public class Themesroot
    {
        public Themes Themes { get; set; }
    }

    public class Themes
    {
        public Theme Love { get; set; }
        public Theme Friendship { get; set; }
        public Theme Revenge { get; set; }
        public Theme Discovery { get; set; }
        public Theme Redemption { get; set; }

        public List<string> GetAllThemes()
        {
            //use the helper method to get a random item from each list
            return new List<string> { Helper.GetRandom(Love.Keywords), Helper.GetRandom(Friendship.Keywords), Helper.GetRandom(Revenge.Keywords), Helper.GetRandom(Discovery.Keywords), Helper.GetRandom(Redemption.Keywords) };
        }
    }

    public class Theme
    {
        public string Description { get; set; }
        public List<string> Keywords { get; set; }
    }

}
