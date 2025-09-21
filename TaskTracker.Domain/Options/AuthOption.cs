namespace TaskTracker.Domain.Options;

public class AuthOption
{
    public string TokenPrivateKey { get; set; } 
    public int ExpiredIntervalMinutes { get; set; }
}
