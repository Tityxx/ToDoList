using UnityEngine;

/// <summary>
/// Класс, хранящий информацию о задаче.
/// </summary>
public class Task : AbstractSerializableClass
{
    public string dateStart;
    public string dateEnd;
    public string name;
    public string info;
    public int priority;
    public bool isDone;
    public string[] tags;
    public string token;

    public Task(string dateStart, string dateEnd, string name, string info, int priority, bool isDone, string[] tags, string token)
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

    /// <summary>
    /// Возвращает объект типа Task из json формата
    /// </summary>
    /// <param name="json">Строка в json формате</param>
    /// <returns></returns>
    public static Task CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<Task>(json);
    }
}
