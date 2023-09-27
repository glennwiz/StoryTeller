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
        string primer;
        Console.WriteLine("You chose ChatBot.");
        var primerORg = "Transcript of a professional dialogue between Developer1 and Developer2. They are discussing about a project they are both working on.\r\n\r\nDeveloper1: Hello Developer2! How's the project coming along?\r\nDeveloper2: Hi Developer1! Things are progressing well, we're on track. How about your tasks?\r\nDeveloper1: I've been tackling a tricky bug, but making headway. Any updates from client side?\r\nDeveloper2: Yes, they just approved the last set of features we submitted!\r\nDeveloper1:";
        primer =
            "AI: Hello. I am an intelligent AI chatbot that answers questions step-by-step and re-evaluates my answers to ensure accuracy. What is your name?\nUser: My name is Tatsu.\nAI: Hello Tatsu, What is your first question or questions?\nUser:";
        mode3 = Mode.ChatBot;
        return primer;
    }

    public static void Chat(string prompt)
    {
        while (true)
        {
            foreach (var text in Session.ChatSession.Chat(prompt,
                         new InferenceParams() { Temperature = 1.0f, AntiPrompts = new List<string> { "User:" } }))
            {
                Console.Write(text);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            prompt = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}