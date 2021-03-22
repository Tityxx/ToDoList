using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpdateTask : MonoBehaviour
{
    public void Test()
    {
        string date = "2021-03-23 00:08";
        //Debug.Log(DateTime.Now.ToString("o"));
        Debug.Log(DateTime.Parse(date));
    }
}
