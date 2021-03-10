using System;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацию о пользователе.
/// </summary>
[Serializable]
public class Tag : AbstractSerializableClass
{
    public string id;
    public string tag;
    public string userId;

    public Tag(string id, string tag, string userId)
    {
        this.id = id;
        this.tag = tag;
        this.userId = userId;
    }
}
