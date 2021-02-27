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

    private const string url = "https://task-list-for-nstu.herokuapp.com/tasks-api/";
    private const string usersApi = "users";
    private const string emailApi = "email=";
    private const string passwordApi = "&password=";

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
    public void GetAuthorizationRequest()
    {
        StartCoroutine(GetAuthorization(url, login, pass));
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    public void GetRegistrationRequest()
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
            User user = User.CreateFromJSON(uwr.downloadHandler.text);
            Debug.Log("Received: " + uwr.downloadHandler.text);
            Debug.Log(string.Format("Received: {0}, {1}, {2}", user.id, user.email, user.password));
        }
    }


    private bool IsSuccessNumberResponse(long number)
    {
        return number >= 200 && number < 300;
    }
}
