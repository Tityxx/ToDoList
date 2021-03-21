using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Вкл/выкл объектов в зависимости
/// отположения тоггла
/// </summary>
public class ToggleSwitcher : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private List<GameObject> toggleIsOn;
    [SerializeField]
    private List<GameObject> toggleIsOff;

    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(OnValueChange);
    }
    private void InDisable()
    {
        toggle.onValueChanged.RemoveListener(OnValueChange);
    }

    private void OnValueChange(bool isOn)
    {
        foreach (GameObject obj in toggleIsOn)
        {
            obj.SetActive(isOn);
        }
        foreach (GameObject obj in toggleIsOff)
        {
            obj.SetActive(!isOn);
        }
    }
}
