using Bot_test.Classes;
using LLama;
using LLama.Common;

string primer = "default";
Mode mode = Mode.Exit;

int choice = 0;

Console.WriteLine("\nPlease select an option:");
Console.WriteLine("1. ChatBot");
Console.WriteLine("2. Never-ending Story Teller");
Console.WriteLine("3. Exit");

bool result = Int32.TryParse(Console.ReadLine(), out choice);

if (!result)
{
    Console.WriteLine("Invalid input. Please enter the number corresponding to your choice.");
}
else{
    switch (choice){
        case 1:
            Console.WriteLine("You chose ChatBot.");
            primer = "Transcript of a professional dialogue between Developer1 and Developer2. They are discussing about a project they are both working on.\r\n\r\nDeveloper1: Hello Developer2! How's the project coming along?\r\nDeveloper2: Hi Developer1! Things are progressing well, we're on track. How about your tasks?\r\nDeveloper1: I've been tackling a tricky bug, but making headway. Any updates from client side?\r\nDeveloper2: Yes, they just approved the last set of features we submitted!\r\nDeveloper1:";
            mode = Mode.ChatBot;
            break;
        case 2:
            Console.WriteLine("You chose Never-ending Story Teller.");
            
            while(true)
            {
                primer = GetPrimer();
                Console.WriteLine("Generated Primer: " + primer);
                Console.WriteLine("Do you want to use this Primer[y/n]:");
                string response = Console.ReadLine();
                
                if(response.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    mode = Mode.StoryTeller;
                    break;
                }
                else if(!response.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Invalid input. Type 'y' to accept the primer or 'n' to generate a new one");
                }
            }
            break;
        case 3:
            Console.WriteLine("Exiting...");
            mode = Mode.Exit;
            break;
        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }
}

var modelPath = "C:\\dev\\LLMs\\llama-2-7b-chat.ggmlv3.q3_K_L.bin";
var prompt = primer;

// Initialize a chat session //seed 2000, 1.18f
var ex = new InteractiveExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 2048, seed: 2001, gpuLayerCount: 5)));
var session = new ChatSession(ex);

Console.WriteLine();
//Write the primer
Console.Write(prompt);

if(mode == Mode.StoryTeller){
    while (prompt != "stop")
    {
        foreach (var text in session.Chat(prompt,
                     new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "\r\n" } }))
        {
            Console.Write(text);
        }

        prompt = ".";
    }
}
else if(mode == Mode.ChatBot){
    while (true)
    {
        foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "Developer1:" } }))
        {
            Console.Write(text);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        prompt = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.White;

    }
}

// save the oki 
session.SaveSession("SavedSessionPath");

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

    var generatedText = sentenceGenerator.GenerateSentence(emotions, genres, timePeriods, characters, actions, objects, settings, plotTwists, themes);
    return generatedText;
}

enum Mode
{
    ChatBot,
    StoryTeller,
    Exit
}