using Bot_test.Classes;
using LLama;
using LLama.Common;

string pathToActionsJson = "Actions.json";
string pathToCharactersJson = "Characters.json";
string pathToEmotionsJson = "Emotions.json";
string pathToGenresJson = "Genres.json";
string pathToObjectsJson = "Objects.json";
string pathToPlotTwistsJson = "PlotTwists.json";
string pathToSettingsJson = "Settings.json";
string pathToThemesJson = "Themes.json";
string pathToTimePeriodsJson = "TimePeriods.json";


var actions = Bot_test.DataLoader.Load<Actions>(pathToActionsJson);
var characters = Bot_test.DataLoader.Load<CharRoot>(pathToCharactersJson);
var emotions = Bot_test.DataLoader.Load<Emotions>(pathToEmotionsJson);
var genres = Bot_test.DataLoader.Load<GenereRoot>(pathToGenresJson);
var objects = Bot_test.DataLoader.Load<Objects>(pathToObjectsJson);
var plotTwists = Bot_test.DataLoader.Load<PlotTwists>(pathToPlotTwistsJson);
var settings = Bot_test.DataLoader.Load<SettingsRoot>(pathToSettingsJson);
var themes = Bot_test.DataLoader.Load<Themesroot>(pathToThemesJson);
var timePeriods = Bot_test.DataLoader.Load<TimePeriods>(pathToTimePeriodsJson);

var sentenceGenerator = new SentenceGenerator();

var generatedText = sentenceGenerator.GenerateSentence(emotions, genres, timePeriods, characters, actions, objects, settings, plotTwists, themes);

string modelPath = "C:\\Users\\Glennwiz\\AppData\\Local\\nomic.ai\\GPT4All\\wizardLM-13B-Uncensored.ggmlv3.q4_0.bin";
var prompt = generatedText;

// Initialize a chat session //seed 2000, 1.18f
var ex = new InteractiveExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 2048, seed: 2001, gpuLayerCount: 5)));
ChatSession session = new ChatSession(ex);

// show the prompt
Console.WriteLine();
Console.Write(prompt);

// run the inference in a loop to chat with LLM
while (prompt != "stop")
{
    foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 1.1f, AntiPrompts = new List<string> { "\r\n" } }))
    {
        Console.Write(text);
    }

    prompt = ".";
    //var key = Console.ReadKey(intercept: true); // Read the key without displaying it
    //if (key.KeyChar == '.')
    //{
    //    prompt = Console.ReadLine();
    //}
    //else if (key.KeyChar != '\r' && key.KeyChar != '\n') // To ensure that only characters other than Enter are processed
    //{
    //    prompt += key.KeyChar; // Build the prompt string until 'stop' is recognized
    //}
}

// save the oki 
session.SaveSession("SavedSessionPath");