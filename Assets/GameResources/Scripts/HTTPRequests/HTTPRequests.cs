using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Основной класс, реализующий HTTP запросы.
/// </summary>
public class HTTPRequests : MonoBehaviour
{
    public static HTTPRequests Instance;

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
    private const string tasksApi = "tasks/";

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

    public void Test()
    {
        //string json = "{\"dateStart\":\"start\",\"dateEnd\":\"end\",\"name\":\"name\",\"info\":\"info\",\"priority\":10,\"isDone\":false,\"tags\":[\"arr1\",\"arr2\",\"arr3\"],\"token\":\"token\"}";
        //Task task = Task.CreateFromJSON(json);
        string[] arr = { "123", "321" };
        tasks = new Task[2];
        tasks[0] = new Task("start1", "end1", "name1", "info1", 5, false, arr, "token1");
        tasks[1] = new Task("start2", "end2", "name2", "info2", 10, true, arr, "token2");

        Task[] tasks2 = JsonHelper.CreateArrayFromJSON<Task>(JsonHelper.SaveArrayToJson(tasks));

        Debug.Log(JsonHelper.SaveArrayToJson(tasks2));
        //StartCoroutine(GetTest(userId));
    }

    private IEnumerator GetTest(string userId)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url + tasksApi + userId);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    public void GetAuthorizationRequest(string login, string pass)
    {
        StartCoroutine(GetAuthorization(url, login, pass));
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    public void GetRegistrationRequest(string login, string pass)
    {
        StartCoroutine(GetRegistration(url, login, pass));
    }

    private IEnumerator GetAuthorization(string url, string email, string password)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url + usersApi + "?" + emailApi + email + passwordApi + password);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    private IEnumerator GetRegistration(string url, string email, string password)
    {
        UnityWebRequest uwr = new UnityWebRequest(url + usersApi + "?" + emailApi + email + passwordApi + password, "POST");
        uwr.downloadHandler = new DownloadHandlerBuffer();

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError || !IsSuccessNumberResponse(uwr.responseCode))
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            User user = User.CreateFromJSON<User>(uwr.downloadHandler.text);
            
        }
    }


    private bool IsSuccessNumberResponse(long number)
    {
        return number >= 200 && number < 300;
    }
}
