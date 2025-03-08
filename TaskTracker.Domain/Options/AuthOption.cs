namespace TaskTracker.Domain.Options;

public class AuthOption
{
    public required string TokenPrivateKey { get; set; }

    public int ExpiredIntervalMinutes { get; set; }
}
