using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    /// <summary>
    /// Преобразование массива json в массив с объектами класса T
    /// </summary>
    /// <typeparam name="T">Тип класса</typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T[] CreateArrayFromJSON<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(FixJson(json));
        return wrapper.Items;
    }


    /// <summary>
    /// Преобразование массива с объектами класса T в формат json
    /// </summary>
    /// <param name="arr">массив</param>
    /// <returns></returns>
    public static string SaveArrayToJson(AbstractSerializableClass[] arr)
    {
        string str = string.Empty;

        for (int i = 0; i < arr.Length; i++)
        {
            str += arr[i].SaveToJson();

            if (i != arr.Length - 1)
            {
                str += ",";
            }
        }

        return "[" + str + "]";
    }

    private static string FixJson(string json)
    {
        return "{\"Items\":" + json + "}";
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}