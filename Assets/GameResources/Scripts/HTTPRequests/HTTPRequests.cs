using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using SimpleJSON;

/// <summary>
/// Основной класс, реализующий HTTP запросы.
/// </summary>
public class HTTPRequests : MonoBehaviour
{
    public static HTTPRequests Instance;

    public Action<bool, Token> AuthResponse;
    public Action<bool> RegstrResponse;

    public Action<bool, Tag> TagAddResponse;
    public Action<bool, List<Tag>> TagGetResponse;
    public Action<bool> TagDelResponse;

    public Action<bool, Task> TaskAddResponse;
    public Action<bool> TaskRewriteResponse;
    public Action<bool, List<Task>> TaskGetResponse;
    public Action<bool> TaskDelResponse;

    [SerializeField]
    private string login = string.Empty;
    [SerializeField]
    private string pass = string.Empty;
    [SerializeField]
    private Task[] tasks;

    private const string url = "https://task-list-for-nstu.herokuapp.com/tasks-api/";
    private const string usersApi = "users";
    private const string emailApi = "email=";
    private const string passwordApi = "&password=";
    private const string tasksApi = "tasks";
    private const string tagsApi = "tags";
    private const string tokenApi = "token=";
    private const string tagQueryParam = "tag=";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    public void GetAuthorizationRequest(User user)
    {
        StartCoroutine(GetAuthorization(url, user.email, user.password));
    }

    private IEnumerator GetAuthorization(string url, string email, string password)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url + usersApi + "?" + emailApi + email + passwordApi + password);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            AuthResponse?.Invoke(false, null);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            AuthResponse?.Invoke(true, Token.CreateFromJSON<Token>(uwr.downloadHandler.text));
            Debug.Log("Success authorization");
            Debug.Log(uwr.downloadHandler.text);
        }
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    public void GetRegistrationRequest(User user)
    {
        StartCoroutine(GetRegistration(url, user));
    }

    private IEnumerator GetRegistration(string url, User user)
    {
        UnityWebRequest uwr = new UnityWebRequest(url + usersApi, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(user.SaveToJson());
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            RegstrResponse?.Invoke(false);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            RegstrResponse?.Invoke(true);
            Debug.Log("Success registration");
        }
    }

    /// <summary>
    /// Удаление тега
    /// </summary>
    public void DelTagRequest(string tagId, string token)
    {
        StartCoroutine(DelTag(url, tagId, token));
    }

    private IEnumerator DelTag(string url, string tagId, string token)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(url + tagsApi + "/" + tagId + "?" + tokenApi + token);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            TagDelResponse?.Invoke(false);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            TagDelResponse?.Invoke(true);
            Debug.Log("Tag deleted");
        }
    }

    /// <summary>
    /// Получение тегов
    /// </summary>
    public void GetTagsRequest(string token)
    {
        StartCoroutine(GetTags(url, token));
    }

    private IEnumerator GetTags(string url, string token)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url + tagsApi + "/" + token);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            TagGetResponse?.Invoke(false, null);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            TagGetResponse?.Invoke(false, new List<Tag>(JsonHelper.CreateArrayFromJSON<Tag>(uwr.downloadHandler.text)));
            Debug.Log("Get tags");
        }
    }

    /// <summary>
    /// Добавление тега
    /// </summary>
    public void AddTagRequest(string tag, string token)
    {
        StartCoroutine(AddTag(url, tag, token));
    }

    private IEnumerator AddTag(string url, string tag, string token)
    {
        UnityWebRequest uwr = new UnityWebRequest(url + tagsApi + "?" + tagQueryParam + tag + "&" + tokenApi + token, "POST");
        uwr.downloadHandler = new DownloadHandlerBuffer();

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            TagAddResponse?.Invoke(false, null);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            TagAddResponse?.Invoke(true, Tag.CreateFromJSON<Tag>(uwr.downloadHandler.text));
            Debug.Log("Add tag");
        }
    }

    /// <summary>
    /// Удаление задачи
    /// </summary>
    public void DelTaskRequest(string taskId, string token)
    {
        StartCoroutine(DelTask(url, taskId, token));
    }

    private IEnumerator DelTask(string url, string taskId, string token)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(url + tasksApi + "/" + taskId + "?" + tokenApi + token);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            TaskDelResponse?.Invoke(false);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            TaskDelResponse?.Invoke(true);
            Debug.Log("Task deleted");
        }
    }

    /// <summary>
    /// Редактирование задачи
    /// </summary>
    public void PutTaskRequest(Task task)
    {
        StartCoroutine(PutTask(url, task));
    }

    private IEnumerator PutTask(string url, Task task)
    {
        UnityWebRequest uwr = UnityWebRequest.Put(url + tasksApi, task.SaveToJson());
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            TaskRewriteResponse?.Invoke(false);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            TaskRewriteResponse?.Invoke(true);
            Debug.Log("Task rewrite");
        }
    }

    /// <summary>
    /// Получение задач
    /// </summary>
    public void GetTasksRequest(string token)
    {
        StartCoroutine(GetTasks(url, token));
    }

    private IEnumerator GetTasks(string url, string token)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url + tasksApi + "/" + token);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            TaskGetResponse?.Invoke(false, null);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            TaskGetResponse?.Invoke(false, new List<Task>(JsonHelper.CreateArrayFromJSON<Task>(uwr.downloadHandler.text)));
            Debug.Log("Get tasks");
        }
    }

    /// <summary>
    /// Добавление задачи
    /// </summary>
    public void AddTaskRequest(UnknownTask task)
    {
        StartCoroutine(AddTask(url, task));
    }

    private IEnumerator AddTask(string url, UnknownTask task)
    {
        UnityWebRequest uwr = new UnityWebRequest(url + tasksApi, "POST");
        uwr.downloadHandler = new DownloadHandlerBuffer();

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            TaskAddResponse?.Invoke(false, null);
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            TaskAddResponse?.Invoke(true, Task.CreateFromJSON<Task>(uwr.downloadHandler.text));
            Debug.Log("Add task");
        }
    }

    private bool IsSuccessNumberResponse(long number)
    {
        return number >= 200 && number < 300;
    }
}
