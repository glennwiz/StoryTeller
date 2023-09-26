using Newtonsoft.Json;

namespace StoryTeller;

public class LanguageModelTests
{
    readonly string json = File.ReadAllText("language_model_tests.json");
    
    private int index;
    private readonly List<string> prompts;
    
    public LanguageModelTests()
    {
        ResetIndex();
        dynamic data = JsonConvert.DeserializeObject(json) ?? throw new InvalidOperationException();
        prompts = data["prompts"].ToList();
    }
    
    private void ResetIndex()
    {
        index = 0;
    }

    public string GetNextString()
    {
        return prompts[index++];
    }
}