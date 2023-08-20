namespace Bot_test.Classes
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
    }

    public class Theme
    {
        public string Description { get; set; }
        public List<string> Keywords { get; set; }
    }

}
