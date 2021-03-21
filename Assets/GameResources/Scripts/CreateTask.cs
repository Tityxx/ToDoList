using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Создание задачи из UI
/// </summary>
public class CreateTask : MonoBehaviour
{
    public List<string> tagsList = new List<string>();

    [SerializeField]
    private Text taskName;
    [SerializeField]
    private Text info;
    [SerializeField]
    private Text dateStart;
    [SerializeField]
    private Text dateEnd;
    [SerializeField]
    private Text priority;
    [SerializeField]
    private GameObject parentForTags;
    [SerializeField]
    private Toggle isDone;
    [SerializeField]
    private Toggle toggle;

    [SerializeField]
    private TasksContainer tasksContainer;

    /// <summary>
    /// Создание задачи
    /// </summary>
    public void CreateTasks()
    {
        HTTPRequests.Instance.TaskAddResponse += CreateTaskRequest;
        string currDateStart = dateStart.text.Equals("") ? DateTime.UtcNow.ToString("o") : dateStart.text;
        HTTPRequests.Instance.AddTaskRequest(new UnknownTask(currDateStart, dateEnd.text, 
            taskName.text, info.text, Int32.Parse(priority.text),
            isDone.isOn, tagsList.ToArray(), tasksContainer.userId));
    }

    private void CreateTaskRequest(bool state, Task task)
    {
        if (state)
        {
            tasksContainer.Tasks.Add(task);
            tasksContainer.CreateTasksOnUI();
        }

        toggle.isOn = false;

        HTTPRequests.Instance.TaskAddResponse -= CreateTaskRequest;
    }
}
