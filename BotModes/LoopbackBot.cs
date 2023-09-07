using System.Net.NetworkInformation;
using System.Text;
using LLama.Common;

namespace StoryTeller.BotModes;

public class LoopbackBot : IMode
{
    public static string LoopbackPrimer(out Mode mode)
    {
        string primer;
        Console.WriteLine("You chose Loopback bot.");
        primer = Prompt.WizardPrompt;
        mode = Mode.LoopbackBot;
        return primer;
    }
    
    private static int hitCounter = 0;
    
    public void StoryTeller(string prompt)
    {
        var stringToPrint = "";
        while (prompt != "stop")
        {
            stringToPrint = "";

            var chat = Session.ChatSession?.Chat(prompt,
                new InferenceParams() {Temperature = 0.95f, AntiPrompts = new List<string> {"\r\n Alien:"}});
            
            foreach (var text in chat)
            {
                var cleanedText = Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(text));
                Console.Write(cleanedText);
                stringToPrint += cleanedText;
            }
            Session.LoggingService.LogMessage(stringToPrint);
            var reply = "";
            hitCounter++;
            if (hitCounter % 2 == 0)
            {
                
                var randomString = SentenceGenerator.GetRandomString();
                reply = randomString + "\n\rAngel:";
            }
            else
            {
                reply = "";
            }
            Console.WriteLine(reply);
            prompt = reply;
        }
    }
}