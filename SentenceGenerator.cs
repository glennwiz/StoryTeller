using StoryTeller.Classes;

namespace StoryTeller
{
    public class SentenceGenerator
    {
        private static string? GetRandomElement<T>(List<T> list)
        {
            if (list.Count == 0) throw new ArgumentException("The list is empty");
            var element = list[Helper.random.Next(list.Count)];

            return element.ToString();
        }

        public string GenerateSentence(Emotions emotions, GenereRoot genres, TimePeriods timePeriods, CharRoot characters, Actions actions, Objects objects, SettingsRoot settings, PlotTwists plotTwists, Themesroot themes, string? name = null)
        {
            var genre = GetRandomElement(genres.Genres.GetAllGenres().SelectMany(g => g.Elements).ToList());
            var timePeriod = GetRandomElement(timePeriods.GetAllTimePeriods());

            var character = name;
            if(name is null)
                character = GetRandomElement(characters.Characters.Protagonists.Names);
            
            var emotion = GetRandomElement(emotions.GetAllEmotions()); 
            var action = GetRandomElement(actions.GetRandomActions()); 
            var obj = GetRandomElement(objects.GetAllObjects());
            var setting = GetRandomElement(settings.Settings.GetRandomLocation()); 
            var plotTwist = GetRandomElement(plotTwists.GetAllPlotTwists()); 
            var theme = GetRandomElement(themes.Themes.GetAllThemes()); 
            var templates = new List<string>
            {
                $"In a {genre} tale set in a {timePeriod} world, our protagonist, {character}, is overcome with {emotion}. Amidst the backdrop of {setting}, they {action} a mysterious {obj}. But here's where things take a turn: {plotTwist}. At its heart, this is a story of {theme}.",
                $"{character}, in a {timePeriod} {genre} setting, finds themselves wrestling with {emotion}. Their journey takes a twist when they {action} a {obj} in {setting}. But nothing is as it seems: {plotTwist}. This saga delves deep into {theme}.",
                $"When {character} {action} a {obj} in {setting}, a {timePeriod} {genre} story unfolds. Overwhelmed by {emotion}, a shocking revelation awaits: {plotTwist}. The tale is ultimately about {theme}.",
                $"Set against the {timePeriod} backdrop of {setting}, {character} embarks on a {genre} quest. Emotions run high as {emotion} takes over, especially after they {action} a {obj}. The climax? {plotTwist}. This narrative explores the depth of {theme}.",
                $"{genre} stories from the {timePeriod} often speak of heroes like {character} who feel deep {emotion}. In the heart of {setting}, a significant event occurs when they {action} a {obj}. Yet, the real surprise is {plotTwist}. A touching tale of {theme}.",
                $"Dive into a {genre} world, where {character} is swayed by {emotion}. Their actions, including when they {action} a {obj} in {setting}, set the stage. But brace yourself for the unexpected: {plotTwist}. A narrative shaped by {theme}.",
                $"Imagine a {timePeriod} {genre} story. Here, {character}, filled with {emotion}, decides to {action} a {obj} in the midst of {setting}. As events unfold, they encounter the unexpected: {plotTwist}. It's a tale that resonates with {theme}.",
                $"{character}'s {emotion}-filled journey in a {genre} world, specifically during the {timePeriod}, leads them to {action} a {obj} in {setting}. What they didn't anticipate was the twist: {plotTwist}. This is a tale woven around {theme}.",
                $"The {timePeriod} era sets the scene for this {genre} narrative. At its core is {character}, driven by {emotion}, and a defining moment when they {action} a {obj} in {setting}. Yet, the story's essence is captured in its twist: {plotTwist}. A profound look into {theme}.",
                $"In this {genre} odyssey from the {timePeriod}, {character} stands out. Dominated by feelings of {emotion}, a turning point emerges as they {action} a {obj} in {setting}. But the heart-stopping moment? {plotTwist}. It's an exploration of {theme} at its finest."
            };
            
            var template = templates[Helper.random.Next(templates.Count)];
            var postfixOnChapter = template + "\r\n\r\\r\nChapter 1:\r\n";


            return postfixOnChapter;
        }

        public List<string> Get50Primers(Emotions emotions, GenereRoot genres, TimePeriods timePeriods, CharRoot characters, Actions actions, Objects objects, SettingsRoot settings, PlotTwists plotTwists, Themesroot themes)
        {
            var primers = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                primers.Add(GenerateSentence(emotions, genres, timePeriods, characters, actions, objects, settings, plotTwists, themes));
            }

            return primers;
        }
        
        public static string GetRandomString()
        {
            var pathToActionsJson = "Actions.json";
            var pathToCharactersJson = "Characters.json";
            var pathToEmotionsJson = "Emotions.json";
            var pathToGenresJson = "Genres.json";
            var pathToObjectsJson = "Objects.json";
            var pathToPlotTwistsJson = "PlotTwists.json";
            var pathToSettingsJson = "Settings.json";
            var pathToThemesJson = "Themes.json";
            var pathToTimePeriodsJson = "TimePeriods.json";

            var themes = DataLoader.Load<Themesroot>(pathToThemesJson);
            var genres = DataLoader.Load<GenereRoot>(pathToGenresJson);
            var objects = DataLoader.Load<Objects>(pathToObjectsJson);
            var actions = DataLoader.Load<Actions>(pathToActionsJson);
            var settings = DataLoader.Load<SettingsRoot>(pathToSettingsJson);
            var emotions = DataLoader.Load<Emotions>(pathToEmotionsJson);
            var characters = DataLoader.Load<CharRoot>(pathToCharactersJson);
            var plotTwists = DataLoader.Load<PlotTwists>(pathToPlotTwistsJson);
            var timePeriods = DataLoader.Load<TimePeriods>(pathToTimePeriodsJson);

            var sentenceGenerator = new SentenceGenerator();

            var generatedText = sentenceGenerator.GenerateSentence(emotions, genres, timePeriods, characters, actions,
                objects, settings, plotTwists, themes);
           
            return generatedText;
        }

        public static string GetRandomSettiong()
        {
            var pathToSettingsJson = "Settings.json";
            var settings = DataLoader.Load<SettingsRoot>(pathToSettingsJson);

            var setting = GetRandomElement(settings.Settings.GetRandomLocation()); 
            return setting;
        }

        public static string GetRandomStringWithName(string name)
        {
            var pathToActionsJson = "Actions.json";
            var pathToCharactersJson = "Characters.json";
            var pathToEmotionsJson = "Emotions.json";
            var pathToGenresJson = "Genres.json";
            var pathToObjectsJson = "Objects.json";
            var pathToPlotTwistsJson = "PlotTwists.json";
            var pathToSettingsJson = "Settings.json";
            var pathToThemesJson = "Themes.json";
            var pathToTimePeriodsJson = "TimePeriods.json";

            var themes = DataLoader.Load<Themesroot>(pathToThemesJson);
            var genres = DataLoader.Load<GenereRoot>(pathToGenresJson);
            var objects = DataLoader.Load<Objects>(pathToObjectsJson);
            var actions = DataLoader.Load<Actions>(pathToActionsJson);
            var settings = DataLoader.Load<SettingsRoot>(pathToSettingsJson);
            var emotions = DataLoader.Load<Emotions>(pathToEmotionsJson);
            var characters = DataLoader.Load<CharRoot>(pathToCharactersJson);
            var plotTwists = DataLoader.Load<PlotTwists>(pathToPlotTwistsJson);
            var timePeriods = DataLoader.Load<TimePeriods>(pathToTimePeriodsJson);

            var sentenceGenerator = new SentenceGenerator();

            var generatedText = sentenceGenerator.GenerateSentence(emotions, genres, timePeriods, characters, actions,
                objects, settings, plotTwists, themes, name);
           
            return generatedText;
        }
    }
}
