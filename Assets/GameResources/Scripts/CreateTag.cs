using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Создание тега из UI
/// </summary>
public class CreateTag : MonoBehaviour
{
    public List<string> tagsList = new List<string>();

    [SerializeField]
    private Text tagName;
    [SerializeField]
    private GameObject parentForTags;

    [SerializeField]
    private TagsContainer tagsContainer;

    /// <summary>
    /// Создание задачи
    /// </summary>
    public void CreateTags()
    {
        HTTPRequests.Instance.TagAddResponse += CreateTagRequest;
        HTTPRequests.Instance.AddTagRequest(tagName.text, tagsContainer.userId);
    }

    private void CreateTagRequest(bool state, Tag tag)
    {
        if (state)
        {
            tagsContainer.Tags.Add(tag);
            tagsContainer.CreateTagsOnUI();
        }

        HTTPRequests.Instance.TagAddResponse -= CreateTagRequest;
    }
}
