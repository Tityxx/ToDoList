using UnityEngine;

/// <summary>
/// Класс, хранящий информацию о пользователе.
/// </summary>
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

    /// <summary>
    /// Возвращает объект типа User из json формата
    /// </summary>
    /// <param name="json">Строка в json формате</param>
    /// <returns></returns>
    public static User CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<User>(json);
    }
}
