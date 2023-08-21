using LLama;
using LLama.Common;
using Logging;
using Newtonsoft.Json;
using StoryTeller.BotModes;

namespace StoryTeller;
public class Sessions
{
    [JsonIgnore]
    public static List<Sessions> AllSessions { get; } = new List<Sessions>();
    [JsonIgnore]
    public static ChatSession? ChatSession { get; set; }
    [JsonIgnore]
    public static LoggerService LoggingService { get; set; }


    public string Primer { get; set; } = string.Empty;
    public string Username { get; set; }
    public Dictionary<string, string> Prompts { get; set; } = new Dictionary<string, string>();

    public Sessions(string username)
    {
        Username = username;
        AllSessions.Add(this);
    }

    public static string LastReply { get; private set; } = string.Empty;

    bool running = false;
    public string GenerateReplyForDiscord(string primer, string message, string discordUsername)
    {
        if (running == true)
        {
            LoggingService.LogMessage($"User: {discordUsername} Message: " + message);
            primer = message;
        }
        else
        {
            LoggingService.LogMessage($"User: {discordUsername} Primer: " + primer);
        }
        
        running = true;
        var reply = "";

        var chat = ChatSession?.Chat(primer, new InferenceParams() {Temperature = 0.9f, AntiPrompts = new List<string> {discordUsername + ":"}});
        foreach (var text in chat)
        {
            Console.Write(text);
            reply += text;
        }
        
        LastReply = reply;
        
        LoggingService.LogMessage(reply);
        return reply;
    }

    public void SaveSession()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sessions", $"{this.Username}.json");
        string jsonContent = JsonConvert.SerializeObject(this);
        File.WriteAllText(path, jsonContent);
    }

    public static ChatSession CreateSession(string prompt, string modelPath)
    {
        DateTime dateTimeForSeed = DateTime.Now;
        long seed = dateTimeForSeed.Ticks;

        Random random = new Random((int)seed);
        var seeNext = random.Next();

        var ex = new InteractiveExecutor(
            new LLamaModel(new ModelParams(modelPath, contextSize: 2048, seed: seeNext, gpuLayerCount: 5)));
        var chatSession = new ChatSession(ex);
        ChatSession = chatSession;

        Console.WriteLine();

        //Write the primer
        Console.Write(prompt);
        return chatSession;
    }

    public static void ModeStart(IMode? botMode, string primer, Mode mode, string prompt)
    {
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
            var se = new Sessions("DarkMage");
            //start on own thread
            DiscordBot? discordBot3 = botMode as DiscordBot;
            discordBot3!.DiscordBotStart(primer);
        }
    }
}