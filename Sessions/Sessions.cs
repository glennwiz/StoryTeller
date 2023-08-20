using LLama;
using LLama.Common;
using Newtonsoft.Json;
using Logging;

namespace StoryTeller;

public class Sessions
{
    [JsonIgnore]
    public static List<Sessions> AllSessions { get; } = new List<Sessions>();
    [JsonIgnore]
    public static ChatSession? ChatSession { get; set; }
    [JsonIgnore]
    public static LoggerService LoggingService { get; set; }  
    
    
    public string Username { get; set; }
    public Dictionary<string, string> Prompts { get; set; } = new Dictionary<string, string>();
    
    public Sessions(string username)
    {
        Username = username;
        AllSessions.Add(this);
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

        Random random = new Random((int) seed);
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
}