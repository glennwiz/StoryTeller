using System.Text;
using LLama;
using LLama.Common;
using Logging;
using Newtonsoft.Json;
using StoryTeller.BotModes;

namespace StoryTeller;
public class Session
{
    [JsonIgnore]
    public static List<Session> AllSessions { get; } = new List<Session>();
    [JsonIgnore]
    public static ChatSession? ChatSession { get; set; }
    [JsonIgnore]
    public static LoggerService LoggingService { get; set; }

    public string Username { get; set; }


    public Session(string username)
    {
        Username = username;
        AllSessions.Add(this);
    }

    public static void CreateSession(string prompt)
    {
        var seed = RandomGenHelper.GenerateRandomNumber();

        var modelPath = @"D:\AI-GPT\LLMs\mistral-7b-v0.1.Q5_K_M.gguf";

        //1create a new model params object
        var @params = new ModelParams(modelPath)
        {
            ContextSize = 4096,
            Seed = seed,
            GpuLayerCount = 5
        };
        
        //2load the model weights
        var weights = LLamaWeights.LoadFromFile(@params);
        
        //3create a new context
        var context = weights.CreateContext(@params);
        
        //4initialize the context with the weights
        var ex = new InteractiveExecutor(context);
        var chatSession = new ChatSession(ex);
        ChatSession = chatSession;
        
        Console.WriteLine("Loading model...");

        //Write the primer
        Console.Write(prompt);
    }

    public static void ModeStart(IMode? botMode, Mode mode, string prompt)
    {
        if (mode == Mode.StoryTeller)
        {
            botMode!.StoryTeller(prompt);
        }
        else if (mode == Mode.ChatBot)
        {
            botMode!.StoryTeller(prompt);
        }
        else if (mode == Mode.DiscordBot)
        {
            botMode!.StoryTeller(prompt);
        }
        else if (mode == Mode.LoopbackBot)
        {
            botMode!.StoryTeller(prompt);
        }
    }
}