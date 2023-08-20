namespace Bot_test.Classes
{
    public class SentenceGenerator
    {
        // Assuming that you have already defined and initialized the classes (like Emotions, Genres, etc.) with data

        private Random random = new Random();

        public string GenerateSentence(Emotions emotions, GenereRoot genres, TimePeriods timePeriods, CharRoot characters, Actions actions, Objects objects, SettingsRoot settings, PlotTwists plotTwists, Themesroot themes)
        {
            string genre = genres.Genres.GetType().GetProperties().Select(prop => prop.Name).ToList()[random.Next(genres.GetType().GetProperties().Length)];
            string timePeriod = timePeriods.GetType().GetProperties().Select(prop => prop.Name).ToList()[random.Next(timePeriods.GetType().GetProperties().Length)];
            string character = characters.Characters.Protagonists.Names[random.Next(characters.Characters.Protagonists.Names.Count)];
            string emotion = emotions.PositiveEmotions.Concat(emotions.NegativeEmotions).Concat(emotions.NeutralEmotions).ToList()[random.Next(emotions.PositiveEmotions.Count + emotions.NegativeEmotions.Count + emotions.NeutralEmotions.Count)];
            string action = actions.CombatMoves.Concat(actions.TravelMethods).Concat(actions.Interactions).ToList()[random.Next(actions.CombatMoves.Count + actions.TravelMethods.Count + actions.Interactions.Count)];
            string obj = objects.MagicalItems.Concat(objects.EverydayItems).ToList()[random.Next(objects.MagicalItems.Count + objects.EverydayItems.Count)];
            string setting = settings.Settings.UrbanLocations.Cities.Concat(settings.Settings.RuralLocations.Villages).ToList()[random.Next(settings.Settings.UrbanLocations.Cities.Count + settings.Settings.RuralLocations.Villages.Count)];
            string plotTwist = plotTwists.Revelations.Concat(plotTwists.Betrayals).ToList()[random.Next(plotTwists.Revelations.Count + plotTwists.Betrayals.Count)];
            // get a random theme
            string theme = themes.Themes.GetType().GetProperties().Select(prop => prop.Name).ToList()[random.Next(themes.Themes.GetType().GetProperties().Length)];





            return $"In a {genre} story set during {timePeriod}, {character} felt {emotion} when they {action} a {obj} in {setting}. The twist? {plotTwist}. It's a tale of {theme}.";
        }
    }
}
