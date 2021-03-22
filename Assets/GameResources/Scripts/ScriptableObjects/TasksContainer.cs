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
    public GameObject updateTaskWindow;

    public string LastUpdatedTaskId
    {
        get
        {
            return lastUpdatedTaskId;
        }
        set
        {
            lastUpdatedTaskId = value;
            updateTaskWindow.SetActive(true);
        }
    }

    private string lastUpdatedTaskId = string.Empty;

    [HideInInspector]
    public Transform parentForTasks;
    [HideInInspector]
    public Text taskTextField;

    /// <summary>
    /// Создание задач в UI
    /// </summary>
    public void CreateTasksOnUI()
    {
        RemoveAllChild();

        foreach (Task task in Tasks)
        {
            GameObject goTask = Instantiate(taskPrefab, parentForTasks);
            TaskWrapper taskWrapper = goTask.GetComponent<TaskWrapper>();
            taskWrapper.Task = new Task(task.id, task.dateStart, task.dateEnd, task.name, task.info, task.priority, task.isDone, task.tags, task.userId);
            taskWrapper.taskTextField = taskTextField;
            taskWrapper.Init();
        }
    }

    /// <summary>
    /// Удаление таски из контейнера и запрос на удаление
    /// </summary>
    /// <param name="taskId"></param>
    public void DelTask(string taskId)
    {
        HTTPRequests.Instance.DelTaskRequest(taskId, userId);

        for (int i = 0; i < Tasks.Count; i++)
        {
            if (Tasks[i].id.Equals(taskId))
            {
                Tasks.Remove(Tasks[i]);
            }
        }
    }

    private void RemoveAllChild()
    {
        int childs = parentForTasks.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(parentForTasks.GetChild(i).gameObject);
        }
    }
}
