using LLama.Common;
using StoryTeller;

namespace Bot_test.Classes;

public class StoryTellerBot : IMode
{
    public void StoryTeller(string s)
    {
        while (s != "stop")
        {
            foreach (var text in Sessions.ChatSession.Chat(s,
                         new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "\r\n" } }))
            {
                Console.Write(text);
            }

            s = ".";
        }
    }
    
    public static string NeverEndingStoryTellerPrimer(out Mode mode2)
    {
        string s1;

        Console.WriteLine("You chose Never-ending Story Teller.");

        while (true)
        {
            s1 = GetPrimer();
            Console.WriteLine("Generated Primer: " + s1);
            Console.WriteLine("Do you want to use this Primer[y/n]:");
            string response = Console.ReadLine();

            if (response.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                mode2 = Mode.StoryTeller;
                break;
            }
            else if (!response.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid input. Type 'y' to accept the primer or 'n' to generate a new one");
            }
        }

        return s1;

        static string GetPrimer()
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

            var themes = Bot_test.DataLoader.Load<Themesroot>(pathToThemesJson);
            var genres = Bot_test.DataLoader.Load<GenereRoot>(pathToGenresJson);
            var objects = Bot_test.DataLoader.Load<Objects>(pathToObjectsJson);
            var actions = Bot_test.DataLoader.Load<Actions>(pathToActionsJson);
            var settings = Bot_test.DataLoader.Load<SettingsRoot>(pathToSettingsJson);
            var emotions = Bot_test.DataLoader.Load<Emotions>(pathToEmotionsJson);
            var characters = Bot_test.DataLoader.Load<CharRoot>(pathToCharactersJson);
            var plotTwists = Bot_test.DataLoader.Load<PlotTwists>(pathToPlotTwistsJson);
            var timePeriods = Bot_test.DataLoader.Load<TimePeriods>(pathToTimePeriodsJson);

            var sentenceGenerator = new SentenceGenerator();

            var generatedText = sentenceGenerator.GenerateSentence(emotions, genres, timePeriods, characters, actions,
                objects, settings, plotTwists, themes);
            return generatedText;
        }
    }
}