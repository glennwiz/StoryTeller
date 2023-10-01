public class Category
{
    public string CategoryName { get; set; }
    public List<string> Prompts { get; set; }
}

public class RootObject
{
    public List<Category> Prompts { get; set; }
}