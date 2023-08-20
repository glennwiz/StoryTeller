using System;
using Logging;
using StoryTeller;
using StoryTeller.BotModes;

public class Program
{
    public static void Main()
    {
        var logger = new LoggerService(@"c:\temp\");
        Sessions.LoggingService = logger;
        Sessions.LoggingService.LogMessage("Program started.");
        
        IMode? botMode = null;
        var primer = "default";
        var mode = Mode.Exit;

        WriteTheSelectionMenu();

        var result = int.TryParse(Console.ReadLine(), out var choice);

        if (!result)
        {
            Console.WriteLine("Invalid input. Please enter the number corresponding to your choice.");
        }
        else
        {
            switch (choice)
            {
                case 1:
                    primer = ChatBot.ChatBotPrimer(out mode);
                    botMode = new ChatBot();
                    break;
                case 2:
                    primer = StoryTellerBot.NeverEndingStoryTellerPrimer(out mode);
                    botMode = new StoryTellerBot();
                    break;
                case 3:
                    primer = DiscordBot.DiscordPrimer(out mode);
                    botMode = new DiscordBot();
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
        

        if (mode == Mode.StoryTeller)
        {
            botMode?.StoryTeller(prompt);
        }
        else if (mode == Mode.ChatBot)
        {
            ChatBot.Chat(prompt);
        }
        else if (mode == Mode.DiscordBot)
        {
            var se= new Sessions("DarkMage");
            //start on own thread
            DiscordBot? discordBot3 = botMode as DiscordBot;
            se.RequestNextString += discordBot3.OnRequestNextStringReceived;
            Task.Run(() => se.StartLoop());
            discordBot3!.DiscordBotStart(primer);
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
    }
}