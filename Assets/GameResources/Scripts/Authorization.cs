using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Авторизация/регистрация
/// </summary>
public class Authorization : AbstractAuthRegstr
{
    [SerializeField]
    private GameObject nextWindow;

    [SerializeField]
    private TasksContainer tasks;

    [SerializeField]
    private GameObject taskPrefab;
    [SerializeField]
    private Transform parentForTasks;
    [SerializeField]
    public Text taskTextField;

    private string errorAuth = "Упс...\nКажется, возникла какая-то ошибка...";

    protected override void Start()
    {
        HTTPRequests.Instance.AuthResponse += GetAuthResponse;
        HTTPRequests.Instance.TaskGetResponse += GetTasksResponse;
    }

    protected override void OnDestroy()
    {
        HTTPRequests.Instance.AuthResponse -= GetAuthResponse;
        HTTPRequests.Instance.TaskGetResponse -= GetTasksResponse;
    }

    /// <summary>
    /// Авторизация
    /// </summary>
    public void DoAuthorization()
    {
        HTTPRequests.Instance.GetAuthorizationRequest(new User(login.text, password.text));
    }

    private void GetAuthResponse(bool state, Token token)
    {
        if (state)
        {
            nextWindow.SetActive(true);
            tasks.userId = token.token;
            DoGetTasks();
        }
        else
        {
            popUpText.text = errorAuth;
            popUpWindow.SetActive(true);
        }
    }

    private void DoGetTasks()
    {
        HTTPRequests.Instance.GetTasksRequest(tasks.userId);
    }

    private void GetTasksResponse(bool state, List<Task> tasksList)
    {
        if (state)
        {
            InitTasks(tasksList);
        }
    }

    private void InitTasks(List<Task> tasksList)
    {
        tasks.Tasks = new List<Task>(tasksList);

        foreach (Task task in tasks.Tasks)
        {
            GameObject goTask = Instantiate(taskPrefab, parentForTasks);
            TaskWrapper taskWrapper = goTask.GetComponent<TaskWrapper>();
            taskWrapper.Task = new Task(task.id, task.dateStart, task.dateEnd, task.name, task.info, task.priority, task.isDone, task.tags, task.userId);
            taskWrapper.taskTextField = taskTextField;
            taskWrapper.Init();
        }
    }
}
