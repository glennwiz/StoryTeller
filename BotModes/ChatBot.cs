using LLama.Common;

namespace StoryTeller.BotModes;

public class ChatBot : IMode
{
    public void StoryTeller(string prompt)
    {
        Chat(prompt);
    }
    
    public static string ChatBotPrimer(out Mode mode3)
    {
        string primer2;
        Console.WriteLine("You chose ChatBot.");
        primer2 =
            "Transcript of a professional dialogue between Developer1 and Developer2. They are discussing about a project they are both working on.\r\n\r\nDeveloper1: Hello Developer2! How's the project coming along?\r\nDeveloper2: Hi Developer1! Things are progressing well, we're on track. How about your tasks?\r\nDeveloper1: I've been tackling a tricky bug, but making headway. Any updates from client side?\r\nDeveloper2: Yes, they just approved the last set of features we submitted!\r\nDeveloper1:";
        mode3 = Mode.ChatBot;
        return primer2;
    }
    
    public static void Chat(string prompt)
    {
        while (true)
        {
            foreach (var text in Session.ChatSession.Chat(prompt,
                         new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "Developer1:" } }))
            {
                Console.Write(text);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            prompt = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}