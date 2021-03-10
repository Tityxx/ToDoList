using System;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацию о задаче.
/// </summary>
[Serializable]
public class UnknownTask : AbstractSerializableClass
{
    public string dateStart;
    public string dateEnd;
    public string name;
    public string info;
    public int priority;
    public bool isDone;
    public string[] tags;
    public string token;

    public UnknownTask(string dateStart, string dateEnd, string name, string info, int priority, bool isDone, string[] tags, string token)
    {
        this.dateStart = dateStart;
        this.dateEnd = dateEnd;
        this.name = name;
        this.info = info;
        this.priority = priority;
        this.isDone = isDone;
        this.token = token;

        this.tags = new string[tags.Length];
        for (int i = 0; i < tags.Length; i++)
        {
            this.tags[i] = tags[i];
        }
    }
}
