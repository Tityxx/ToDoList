using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отображение тегов
/// </summary>
public class TagsViewTagWrapper : MonoBehaviour
{
    [SerializeField]
    private TagsContainer tagsContainer;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private GameObject tagPrefab;

    private void OnEnable()
    {
        tagsContainer.parentForTags = parent;
        ViewTags();
    }

    private void ViewTags()
    {
        RemoveAllChild();
        ViewAllTags();
    }

    private void ViewAllTags()
    {
        foreach (Tag tag in tagsContainer.Tags)
        {
            GameObject goTag = Instantiate(tagPrefab, parent);
            TagWrapper tagWrapper = goTag.GetComponent<TagWrapper>();
            tagWrapper.Tag = tag;
            tagWrapper.Init();
        }
    }

    private void RemoveAllChild()
    {
        int childs = parent.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
