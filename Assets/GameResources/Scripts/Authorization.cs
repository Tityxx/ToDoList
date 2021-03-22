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
    private TagsContainer tags;

    [SerializeField]
    private GameObject taskPrefab;
    [SerializeField]
    private Transform parentForTasks;
    [SerializeField]
    public Text taskTextField;

    [SerializeField]
    public GameObject updateTaskWindow;

    private string errorAuth = "Упс...\nКажется, возникла какая-то ошибка...";

    protected override void Start()
    {
        HTTPRequests.Instance.AuthResponse += GetAuthResponse;
        HTTPRequests.Instance.TaskGetResponse += GetTasksResponse;
        HTTPRequests.Instance.TagGetResponse += GetTagsResponse;
    }

    protected override void OnDestroy()
    {
        HTTPRequests.Instance.AuthResponse -= GetAuthResponse;
        HTTPRequests.Instance.TaskGetResponse -= GetTasksResponse;
        HTTPRequests.Instance.TagGetResponse -= GetTagsResponse;
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
            tags.userId = token.token;
            tasks.updateTaskWindow = updateTaskWindow;
            DoGetTasks();
            DoGetTags();
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
        tasks.parentForTasks = parentForTasks;
        tasks.taskTextField = taskTextField;
        tasks.CreateTasksOnUI();
    }

    private void DoGetTags()
    {
        HTTPRequests.Instance.GetTagsRequest(tags.userId);
    }

    private void GetTagsResponse(bool state, List<Tag> tagsList)
    {
        if (state)
        {
            InitTags(tagsList);
        }
    }

    private void InitTags(List<Tag> tagsList)
    {
        tags.Tags = new List<Tag>(tagsList);
    }
}
