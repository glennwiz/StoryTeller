using System.Diagnostics;
using Bot_test.Classes;
using LLama;
using LLama.Common;
using StoryTeller;
using Newtonsoft.Json;


IMode BotMode = null;
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
            primer = ChatBot.ChatBotPrimer(out mode);
            BotMode = new ChatBot();
            break;
        case 2:
            primer = StoryTellerBot.NeverEndingStoryTellerPrimer(out mode);
            BotMode = new StoryTellerBot();
            break;
        case 3:
            primer = DiscordBot.DiscordPrimer(out mode);
            BotMode = new DiscordBot();
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

var modelPath = @"C:\Users\Glennwiz\AppData\Local\nomic.ai\GPT4All\llama-2-7b-chat.ggmlv3.q4_0.bin";
var prompt = primer;

var session = Sessions.CreateSession(prompt, modelPath);

if(mode == Mode.StoryTeller)
{
    BotMode?.StoryTeller(prompt);
}
else if(mode == Mode.ChatBot)
{
    ChatBot.Chat(prompt);
}
else if(mode == Mode.DiscordBot)
{
    DiscordBotStart();
}

// save the oki 
session.SaveSession("SavedSessionPath");
void DiscordBotStart()
{
    var discordBot = DiscordBotStart();
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





