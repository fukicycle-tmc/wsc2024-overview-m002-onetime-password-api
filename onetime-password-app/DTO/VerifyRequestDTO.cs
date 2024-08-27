
namespace onetime_password_app.DTO;

public sealed class VerifyRequestDTO
{
    public VerifyRequestDTO(Guid guid, string password)
    {
        Guid = guid;
        Password = password;
    }
    public Guid Guid { get; }
    public string Password { get; }
}
