namespace Fundamentals;

public class CustomExceptions
{
    public static void Run()
    {
        try
        {
            var stage = new Stage("", -5);
        }
        catch (InvalidStageException ex)
        {
            Console.WriteLine($"Error creating stage: {ex.Message}");
        }
    }
}

public class InvalidStageException(string message) 
    : Exception(message)
{
}

public class Stage
{
    public string Name { get; private set; }
    public short PlayerCount { get; private set; }

    public Stage(string name, short playerCount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidStageException("Stage name cannot be empty.");

        if (playerCount < 0)
            throw new InvalidStageException("Player count cannot be negative.");

        Name = name;
        PlayerCount = playerCount;
    }
}