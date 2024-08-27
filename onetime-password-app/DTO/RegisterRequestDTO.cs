using System;

namespace onetime_password_app.DTO;

public sealed class RegisterRequestDTO
{
    public RegisterRequestDTO(Guid guid, string firstName, string lastName)
    {
        Guid = guid;
        FirstName = firstName;
        LastName = lastName;
    }
    public Guid Guid { get; }
    public string FirstName { get; }
    public string LastName { get; }
}