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
        string currDateStart = dateStart.text.Equals("") ? DateTime.UtcNow.ToString("o") : DateTime.Parse(dateStart.text).ToString("o");
        string currDateEnd = dateEnd.text.Equals("") ? dateEnd.text : DateTime.Parse(dateEnd.text).ToString("o");
        int priority_ = priority.text.Equals("") ? 1 : Int32.Parse(priority.text);
        HTTPRequests.Instance.AddTaskRequest(new UnknownTask(currDateStart, currDateEnd, 
            taskName.text, info.text, priority_,
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

    /// <summary>
    /// Редактирование задачи
    /// </summary>
    public void UpdateTasks()
    {
        HTTPRequests.Instance.TaskRewriteResponse += UpdateTaskRequest;
        string currDateStart = dateStart.text.Equals("") ? DateTime.UtcNow.ToString("o") : DateTime.Parse(dateStart.text).ToString("o");
        string currDateEnd = dateEnd.text.Equals("") ? dateEnd.text : DateTime.Parse(dateEnd.text).ToString("o");
        int priority_ = priority.text.Equals("") ? 1 : Int32.Parse(priority.text);

        foreach (Task task_ in tasksContainer.Tasks)
        {
            if (task_.id.Equals(tasksContainer.LastUpdatedTaskId))
            {
                task_.dateStart = currDateStart;
                task_.dateEnd = currDateEnd;
                task_.name = taskName.text;
                task_.info = info.text;
                task_.priority = priority_;
                task_.isDone = isDone.isOn;
                task_.tags = tagsList.ToArray();
                HTTPRequests.Instance.PutTaskRequest(task_);
            }
        }
    }

    private void UpdateTaskRequest(bool state)
    {
        if (state)
        {
            tasksContainer.CreateTasksOnUI();
        }

        gameObject.SetActive(false);

        HTTPRequests.Instance.TaskRewriteResponse -= UpdateTaskRequest;
    }
}
