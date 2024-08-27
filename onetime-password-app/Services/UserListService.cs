using System;

namespace onetime_password_app.Services;

public sealed class UserListService
{
    private readonly ILogger<UserListService> _logger;
    public UserListService(ILogger<UserListService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<UserRecord> Get()
    {
        if (Directory.Exists("Debug"))
        {
            List<UserRecord> users = new List<UserRecord>();
            try
            {
                string[] files = Directory.GetFiles("Debug");
                foreach (string file in files)
                {
                    string content = File.ReadAllText(file);
                    UserRecord user = new UserRecord(content, Guid.Parse(Path.GetFileNameWithoutExtension(file)));
                    _logger.LogInformation("Added: {0}", user.ToString());
                    users.Add(user);
                }
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        return Enumerable.Empty<UserRecord>();
    }
}

public record UserRecord(string Fullname, Guid Guid);
