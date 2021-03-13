using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Авторизация/регистрация
/// </summary>
public class Registration : AbstractAuthRegstr
{
    private string successReg = "Регистрация успешно завершена.";
    private string errorReg = "Упс...\nКажется, возникла какая-то ошибка...";

    protected override void Start()
    {
        HTTPRequests.Instance.RegstrResponse += GetRegstrResponse;
    }

    protected override void OnDestroy()
    {
        HTTPRequests.Instance.RegstrResponse -= GetRegstrResponse;
    }

    /// <summary>
    /// Регистрация
    /// </summary>
    public void DoRegistration()
    {
        HTTPRequests.Instance.GetRegistrationRequest(new User(login.text, password.text));
    }

    private void GetRegstrResponse(bool state)
    {
        popUpText.text = state ? successReg : errorReg;
        popUpWindow.SetActive(true);
    }
}
