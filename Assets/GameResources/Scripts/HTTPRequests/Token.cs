using System;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацию о токене.
/// </summary>
[Serializable]
public class Token : AbstractSerializableClass
{
    public string token;

    public Token(string token)
    {
        this.token = token;
    }
}
