using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Выход из приложения
/// </summary>
[RequireComponent(typeof(Button))]
public class ExitApp : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Exit);
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
