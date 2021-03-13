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
    [SerializeField]
    private Color choosenColor;
    [SerializeField]
    private Color notChoosenColor;

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

    /// <summary>
    /// Смена прозрачности текста
    /// </summary>
    /// <param name="isOn"></param>
    public void ChangeColor(bool isOn)
    {
        text.color = isOn ? choosenColor : notChoosenColor;
    }
}
