using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Обёртка для тегов
/// </summary>
public class TagWrapper : MonoBehaviour
{
    [HideInInspector]
    public Tag Tag;
    [SerializeField]
    private Text tagtext;

    public void Init()
    {
        tagtext.text = Tag.tag;
    }
}
