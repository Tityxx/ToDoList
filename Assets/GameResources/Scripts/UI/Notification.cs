using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Включение/выключение лоадера
/// </summary>
public class Notification : MonoBehaviour
{
    public static Notification Instance;

    [SerializeField]
    private GameObject window;

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
    /// Вкл/выкл окна лоадера
    /// </summary>
    /// <param name="state"></param>
    public void ActivateWindow(bool state)
    {
        window.SetActive(state);
    }
}
