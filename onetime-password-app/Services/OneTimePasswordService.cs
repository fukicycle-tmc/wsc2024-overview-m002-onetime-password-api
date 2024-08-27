namespace onetime_password_app.Services;

public sealed class OneTimePasswordService
{
    private const int PASSWORD_LENGTH = 6;
    private const int ACTUAL_EXPIRED_SECONDS = 35;

    private readonly ILogger<OneTimePasswordService> _logger;
    public OneTimePasswordService(ILogger<OneTimePasswordService> logger)
    {
        _logger = logger;
    }

    private List<OneTimePasswordRecord> _items = new List<OneTimePasswordRecord>();
    public OneTimePasswordRecord Generate(Guid guid)
    {
        string password = string.Join("", GeneratePassword());
        DateTime expired = DateTime.Now.AddSeconds(ACTUAL_EXPIRED_SECONDS);
        OneTimePasswordRecord oneTimePasswordRecord = new OneTimePasswordRecord(guid, password, expired);
        _logger.LogInformation("Added one time password: {0}", oneTimePasswordRecord.ToString());
        _items.Add(oneTimePasswordRecord);
        return oneTimePasswordRecord;
    }

    public bool Verify(Guid guid, string password)
    {
        _logger.LogInformation("Verify one time password: {0}", guid.ToString());
        if (_items.Any(a => a.Guid == guid && a.Password == password && a.Expired >= DateTime.Now))
        {
            return true;
        }
        return false;
    }

    private IEnumerable<char> GeneratePassword()
    {
        string lower = "abcdefghijklmnopqrstuvwxyz";
        string upper = lower.ToUpper();
        for (int i = 0; i < PASSWORD_LENGTH; i++)
        {
            if (Random.Shared.Next() % 2 == 0)
            {
                yield return upper[Random.Shared.Next(0, upper.Length - 1)];
            }
            else
            {
                yield return lower[Random.Shared.Next(0, lower.Length - 1)];
            }
        }
    }

    internal bool Verify(object guid, object password)
    {
        throw new NotImplementedException();
    }
}

public record OneTimePasswordRecord(Guid Guid, string Password, DateTime Expired);
