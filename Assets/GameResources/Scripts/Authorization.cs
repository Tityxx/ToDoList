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

    private string errorAuth = "Упс...\nКажется, возникла какая-то ошибка...";

    protected override void Start()
    {
        HTTPRequests.Instance.AuthResponse += GetAuthResponse;

    }

    protected override void OnDestroy()
    {
        HTTPRequests.Instance.AuthResponse -= GetAuthResponse;
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
        }
        else
        {
            popUpText.text = errorAuth;
            popUpWindow.SetActive(true);
        }
    }
}
