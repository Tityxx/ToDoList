using System;
using UnityEngine;

/// <summary>
/// Абстрактный класс для сериализованных объектов
/// </summary>
[Serializable]
public abstract class AbstractSerializableClass
{
    /// <summary>
    /// Преобразование класса в формат json
    /// </summary>
    /// <returns></returns>
    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

    /// <summary>
    /// Возвращает объект типа T из json формата
    /// </summary>
    /// <param name="json">Строка в json формате</param>
    /// <returns></returns>
    public static T CreateFromJSON<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
