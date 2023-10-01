using LLama.Common;
using Newtonsoft.Json;
using System.Text;

namespace StoryTeller;

public class LanguageModelTests
{
    readonly string json = File.ReadAllText("C:\\dev\\StoryTeller\\Json\\language_model_tests.json");

    private int index;
    private readonly List<string> prompts;

    public LanguageModelTests()
    {
        ResetIndex();
        var root = JsonConvert.DeserializeObject<RootObject>(json);
        prompts = new List<string>();
        foreach (var category in root.Prompts)
        {
            prompts.AddRange(category.Prompts);
        }
    }

    private void ResetIndex()
    {
        index = 0;
    }

    public string GetNextString()
    {
        return prompts[index++];
    }

    internal void Run()
    {
        for (int i = 0; i < prompts.Count; i++)
        {
            var prompt = GetNextString();
            Console.WriteLine($"Prompt: {prompt}");
            var chat = Session.ChatSession?.Chat(prompt,
                               new InferenceParams() { Temperature = 0.95f, AntiPrompts = new List<string> { "\r\n Alien:" } });
            foreach (var text in chat)
            {
                var cleanedText = Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(text));
                Console.Write(cleanedText);
            }
            Console.WriteLine();
        }
    }
}