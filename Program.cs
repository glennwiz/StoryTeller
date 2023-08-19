﻿using System.Diagnostics;
using Bot_test.Classes;
using LLama;
using LLama.Common;
using Newtonsoft.Json;

var primer = "default";
var mode = Mode.Exit;

var choice = 0;

WriteTheSelectionMenu();

var result = int.TryParse(Console.ReadLine(), out choice);

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
            Console.WriteLine("You chose DiscordBot.");
            mode = Mode.DiscordBot;
            break;
        case 4:
            Console.WriteLine("you chose CoderPal.");
            mode = Mode.CoderPal;
            break;
        case 5:
            Console.WriteLine("Exiting...");
            mode = Mode.Exit;
            break;
        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }
}

var modelPath = @"C:\Users\Glennwiz\AppData\Local\nomic.ai\GPT4All\wizardlm-13b-v1.1-superhot-8k.ggmlv3.q4_0.bin";

var prompt = primer;

// Initialize a chat session //seed 2000, 1.18f
var ex = new InteractiveExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 2048, seed: 2001, gpuLayerCount: 5)));
var session = new ChatSession(ex);

Console.WriteLine();
//Write the primer
Console.Write(prompt);

if(mode == Mode.StoryTeller)
{
    StoryTeller(prompt, session);
}
else if(mode == Mode.ChatBot)
{
    ChatBot(session, prompt);
}
else if(mode == Mode.DiscordBot)
{
    DiscordBot(session, prompt);
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
    var debugPrimers = sentenceGenerator.Get50Primers(emotions, genres, timePeriods, characters, actions, objects, settings, plotTwists, themes);
    //debug print the 50 primers
    Debug.WriteLine(JsonConvert.SerializeObject(debugPrimers, Formatting.Indented));
    
    var generatedText = sentenceGenerator.GenerateSentence(emotions, genres, timePeriods, characters, actions, objects, settings, plotTwists, themes);
    return generatedText;
}

void StoryTeller(string s, ChatSession chatSession)
{
    while (s != "stop")
    {
        foreach (var text in chatSession.Chat(s,
                     new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "\r\n" } }))
        {
            Debug.WriteLine("Im outside the foreach loop ");
            Console.Write(text);
        }
        Debug.WriteLine("Im outside the foreach loop ");
        s = ".";
    }
}

void ChatBot(ChatSession session1, string prompt1)
{
    while (true)
    {
        foreach (var text in session1.Chat(prompt1,
                     new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "Developer1:" } }))
        {
            Console.Write(text);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        prompt1 = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.White;
    }
}

void DiscordBot(ChatSession session, string prompt)
{
    throw new NotImplementedException();
}

void WriteTheSelectionMenu()
{
    Console.WriteLine("\nPlease select an option:");
    Console.WriteLine("1. ChatBot");
    Console.WriteLine("2. Never-ending Story Teller");
    Console.WriteLine("3. Discord Bot");
    Console.WriteLine("4. CoderPal");
    Console.WriteLine("5. Exit");
}