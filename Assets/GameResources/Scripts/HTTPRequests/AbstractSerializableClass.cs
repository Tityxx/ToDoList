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
}
