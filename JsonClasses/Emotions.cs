using StoryTeller;

namespace StoryTeller.Classes
{
    public class Emotions
    {
        public List<string> PositiveEmotions { get; set; }
        public List<string> NegativeEmotions { get; set; }
        public List<string> NeutralEmotions { get; set; }

        public List<string> GetAllEmotions()
        {
            //use the helper method to get a random item from each list
            return new List<string> { Helper.GetRandom(PositiveEmotions), Helper.GetRandom(NegativeEmotions), Helper.GetRandom(NeutralEmotions) };
        }
    }
}
