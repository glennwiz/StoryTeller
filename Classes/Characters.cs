namespace StoryTeller.Classes
{
    public class Animals
    {
        public string Description { get; set; }
        public List<string> Names { get; set; }
    }

    public class Antagonists
    {
        public string Description { get; set; }
        public List<string> Names { get; set; }
    }

    public class Characters
    {
        public Protagonists Protagonists { get; set; }
        public Antagonists Antagonists { get; set; }
        public SupportingCharacters SupportingCharacters { get; set; }
        public MysticalBeings MysticalBeings { get; set; }
        public Animals Animals { get; set; }
    }

    public class MysticalBeings
    {
        public string Description { get; set; }
        public List<string> Names { get; set; }
    }

    public class Protagonists
    {
        public string Description { get; set; }
        public List<string> Names { get; set; }
    }

    public class CharRoot
    {
        public Characters Characters { get; set; }
    }

    public class SupportingCharacters
    {
        public string Description { get; set; }
        public List<string> Names { get; set; }
    }
}
