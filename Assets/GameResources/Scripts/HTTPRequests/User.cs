using System;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацию о пользователе.
/// </summary>
[Serializable]
public class User : AbstractSerializableClass
{
    public string email;
    public string password;

    public User(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}
