using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Смена цвета спрайта на тоггле
/// </summary>
public class ToggleImageColorController : MonoBehaviour
{
    [SerializeField]
    private Image image;
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
    /// Смена цвета текста
    /// </summary>
    /// <param name="isOn"></param>
    public void ChangeColor(bool isOn)
    {
        image.color = isOn ? choosenColor : notChoosenColor;
    }
}
