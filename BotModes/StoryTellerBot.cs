using LLama.Common;
using StoryTeller.Classes;

namespace StoryTeller.BotModes;

public class StoryTellerBot : IMode
{
    public void StoryTeller(string s)
    {
        var stringToPrint = "";
        while (s != "stop")
        {
            stringToPrint = "";
            foreach (var text in Sessions.ChatSession.Chat(s,
                         new InferenceParams() { Temperature = 0.8f, AntiPrompts = new List<string> { "." } }))
            {
                Console.Write(text);
                stringToPrint += text;
            }
            Sessions.LoggingService.LogMessage(stringToPrint);
            s = " ";
        }
    }
    
    public static string NeverEndingStoryTellerPrimer(out Mode mode)
    {
        string primer;

        Console.WriteLine("You chose Never-ending Story Teller.");

        while (true)
        {
            primer = GetPrimer();
            Console.WriteLine("Generated Primer: " + primer);
            Sessions.LoggingService.LogMessage("Generated Primer: " + primer);
            Console.WriteLine("Do you want to use this Primer[y/n]:");
            
            var response = Console.ReadLine();
            
            if (response.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                Sessions.LoggingService.LogMessage("Chosen Primer: " + primer);
                mode = Mode.StoryTeller;
                break;
            }
            else if (!response.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid input. Type 'y' to accept the primer or 'n' to generate a new one");
            }
        }

        return primer;

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
    }
}