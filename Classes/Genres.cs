public class Genres
{
    public Genre Fantasy { get; set; }
    public Genre SciFi { get; set; }
    public Genre Mystery { get; set; }
    public Genre Romance { get; set; }
    public Genre Horror { get; set; }
}

public class Genre
{
    public List<string> Elements { get; set; }
    public List<string> CommonSettings { get; set; }
    public List<string> KeyCharacters { get; set; }
}

public class GenereRoot
{
    public Genres Genres { get; set; }
}