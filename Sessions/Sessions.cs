using System.Text;
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

        IEnumerable<string>? chat = ChatSession?.Chat(primer, new InferenceParams()
        {
            MaxTokens = -1, 
            PathSession = @"c:\dev\LLMs\",
            Temperature = 0.9f, /*The temperature controls the degree of randomness in token selection. 
            The temperature is used for sampling during response generation, which occurs when topP and topK are applied. 
            Lower temperatures are good for prompts that require a more deterministic/less open-ended response, while higher temperatures can lead to more diverse or creative results. 
            A temperature of 0 is deterministic, meaning that the highest probability response is always selected.*/
            
            TopK = 100, /* The topK parameter changes how the model selects tokens for output. 
            A topK of 1 means the selected token is the most probable among all the tokens in the model’s vocabulary (also called greedy decoding),
            while a topK of 3 means that the next token is selected from among the 3 most probable using the temperature. 
            For each token selection step, the topK tokens with the highest probabilities are sampled. 
            Tokens are then further filtered based on topP with the final token selected using temperature sampling.*/
            
            TopP = 0.9f, /* The topP parameter changes how the model selects tokens for output. 
            Tokens are selected from the most to least probable until the sum of their probabilities equals the topP value. 
            For example, if tokens A, B, and C have a probability of 0.3, 0.2, and 0.1 and the topP value is 0.5, 
            then the model will select either A or B as the next token by using the temperature and exclude C as a candidate. 
            The default topP value is 0.95.*/
            FrequencyPenalty = -1.2f, /*Negative values can be used to increase the likelihood of repetition.*/
            AntiPrompts = new List<string>
            {
                discordUsername + ":",
                "\u003C/s\u003E", 
                "\n#",
                "\n"+discordUsername+":", 
                "\n*"+discordUsername, 
                "\n\r\n\r\n\r",
                "\u003C/s\u003E\u003C/p\u003E\n\n",
            }
        });
        
        foreach (var text in chat)
        {
            //set utf-8 encoding
            var utf8 = Encoding.UTF8;
            var utfBytes = utf8.GetBytes(text);
            var utf8Text = utf8.GetString(utfBytes, 0, utfBytes.Length);
            
            reply += utf8Text.ToLower();
            Console.Write(text);
        }
        
        LastReply = reply;
        
        LoggingService.LogMessage(reply);
        return reply;
    }

    public void SaveSession()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sessions", $"{this.Username}.json");
        var jsonContent = JsonConvert.SerializeObject(this);
        File.WriteAllText(path, jsonContent);
    }

    public static ChatSession CreateSession(string prompt)
    {
        var dateTimeForSeed = DateTime.Now;
        var seed = dateTimeForSeed.Ticks;

        var random = new Random((int)seed);
        var seeNext = random.Next();
        
        var modelPath = @"c:\dev\LLMs\pygmalion-2-13b.Q2_K.gguf";
        //var modelPath = @"C:\dev\LLMs\llama-2-7b-chat.ggmlv3.q4_0.bin";
        //var modelPath = @"C:\dev\LLMs\Wizard-Vicuna-7B-Uncensored.ggmlv3.q2_K.bin";

        var ex = new InteractiveExecutor(
            new LLamaContext(new ModelParams(modelPath)
            {
             ContextSize   = 2 * 2048,
             Seed = seeNext,
             GpuLayerCount = 5
            }));
        var chatSession = new ChatSession(ex);
        ChatSession = chatSession;
        
        Console.WriteLine("Loading model...");

        //Write the primer
        Console.Write(prompt);
        return chatSession;
    }

    public static void ModeStart(IMode? botMode, string primer, Mode mode, string prompt)
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
            botMode!.StoryTeller(primer);
        }
        else if (mode == Mode.LoopbackBot)
        {
            botMode!.StoryTeller(prompt);
        }
    }
}