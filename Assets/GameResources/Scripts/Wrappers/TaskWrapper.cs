using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Обёртка для задач
/// </summary>
public class TaskWrapper : MonoBehaviour
{
    [HideInInspector]
    public Task Task;
    [HideInInspector]
    public ToggleGroup toggleGroup;
    [HideInInspector]
    public Text taskTextField;

    [SerializeField]
    private Toggle toggle;

    /// <summary>
    /// Установить текстовое поле с инофрмацией 
    /// о задаче при нажатии на неё
    /// </summary>
    /// <param name="isOn"></param>
    public void SetTaskInfo(bool isOn)
    {
        if (isOn)
        {
            taskTextField.text = Task.info;
        }
    }
}
