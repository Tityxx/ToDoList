using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject taskPrefab;

    [HideInInspector]
    public Transform parentForTasks;
    [HideInInspector]
    public Text taskTextField;

    /// <summary>
    /// Создание задач в UI
    /// </summary>
    public void CreateTasksOnUI()
    {
        foreach (Task task in Tasks)
        {
            GameObject goTask = Instantiate(taskPrefab, parentForTasks);
            TaskWrapper taskWrapper = goTask.GetComponent<TaskWrapper>();
            taskWrapper.Task = new Task(task.id, task.dateStart, task.dateEnd, task.name, task.info, task.priority, task.isDone, task.tags, task.userId);
            taskWrapper.taskTextField = taskTextField;
            taskWrapper.Init();
        }
    }
}
