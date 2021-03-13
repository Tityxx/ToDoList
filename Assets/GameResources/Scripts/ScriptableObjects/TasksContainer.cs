using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// SO для хранения тасок и id юзера,
/// под которым авторизовались
/// </summary>
[CreateAssetMenu]
public class TasksContainer : ScriptableObject
{
    public string userId;
    public List<Task> Tasks;

    

}
