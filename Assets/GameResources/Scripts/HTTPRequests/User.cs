using System;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацию о пользователе.
/// </summary>
[Serializable]
public class User : AbstractSerializableClass
{
    public string id;
    public string email;
    public string password;

    public User(string id, string email, string password)
    {
        this.id = id;
        this.email = email;
        this.password = password;
    }
}
