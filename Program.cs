using Logging;
using StoryTeller;
using StoryTeller.BotModes;


public class Program
{
    public static void Main()
    {
        var logger = new LoggerService(@"c:\temp\");
        Session.LoggingService = logger;
        Session.LoggingService.LogMessage("Program started.");

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
            botMode = GetBotModeFromChoice(choice, ref mode, ref primer);
        }

        var prompt = primer;

        Session.CreateSession(prompt);

        Session.ModeStart(botMode, mode, prompt);

        void WriteTheSelectionMenu()
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. ChatBot");
            Console.WriteLine("2. Never-ending Story Teller");
            Console.WriteLine("3. Discord Bot");
            Console.WriteLine("4. CoderPal");
            Console.WriteLine("5. Loopback bot");
            Console.WriteLine("6. TestRunner");
            Console.WriteLine("7. Exit");
        }
    }

    private static IMode? GetBotModeFromChoice(int choice, ref Mode mode, ref string primer)
    {
        IMode? botMode = null;
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
                Console.WriteLine("you chose CoderPal");
                mode = Mode.CoderPal;
                break;
            case 5:
                primer = LoopbackBot.LoopbackPrimer(out mode);
                mode = Mode.LoopbackBot;
                botMode = new LoopbackBot();
                break;
            case 6:
                mode = Mode.TestRunner;
                Session.CreateSession("");
                var languageModelTests = new LanguageModelTests();
                languageModelTests.Run();
                break;
            case 7:
                Console.WriteLine("Exiting...");
                mode = Mode.Exit;
                break;
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }

        return botMode;
    }
}
