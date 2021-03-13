using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Смена цвета текста на тоггле
/// </summary>
public class TextColorController : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ChangeColor);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(ChangeColor);
    }

    public void ChangeColor(bool isOn)
    {
        Color tmpColor = text.color;
        tmpColor.a = isOn ? 1 : 0.5f;
        text.color = tmpColor;
    }
}
