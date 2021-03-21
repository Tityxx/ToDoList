using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Сохранение тегов для новой задачи
/// </summary>
public class SaveTags : MonoBehaviour
{
    [SerializeField]
    private Transform parentForTags;
    [SerializeField]
    private Transform leftParentForTags;
    [SerializeField]
    private GameObject tagPrefab;
    [SerializeField]
    private GameObject textNoTags;
    [SerializeField]
    private CreateTask createTask;

    /// <summary>
    /// Сохранение тегов для новой задачи
    /// </summary>
    public void SaveTags_()
    {
        RemoveAllChild();
        textNoTags.SetActive(true);

        createTask.tagsList = new List<string>();
        for (int i = 0; i < parentForTags.childCount; i++)
        {
            TagWrapper_1 tagWrapper = parentForTags.GetChild(i).gameObject.GetComponent<TagWrapper_1>();
            if (tagWrapper.toggle.isOn)
            {
                createTask.tagsList.Add(tagWrapper.tagName.text);

                GameObject tag = Instantiate(tagPrefab, leftParentForTags);
                TagWrapper_1 tagWrapperHelper = tag.GetComponent<TagWrapper_1>();
                tagWrapperHelper.tagName.text = tagWrapper.tagName.text;

                textNoTags.SetActive(false);
            }
        }

        gameObject.SetActive(false);
    }

    private void RemoveAllChild()
    {
        int childs = leftParentForTags.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(leftParentForTags.GetChild(i).gameObject);
        }
    }
}
