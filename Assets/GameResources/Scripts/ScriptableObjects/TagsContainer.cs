using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SO для хранения тегов и id юзера,
/// под которым авторизовались
/// </summary>
[CreateAssetMenu]
public class TagsContainer : ScriptableObject
{
    public string userId;
    public List<Tag> Tags;
    public GameObject tagPrefab;

    [HideInInspector]
    public Transform parentForTags;

    /// <summary>
    /// Создание тега в UI
    /// </summary>
    public void CreateTagsOnUI()
    {
        RemoveAllChild();

        foreach (Tag tag in Tags)
        {
            GameObject goTag = Instantiate(tagPrefab, parentForTags);
            TagWrapper tagWrapper = goTag.GetComponent<TagWrapper>();
            tagWrapper.Tag = new Tag(tag.id, tag.tag, userId);
            tagWrapper.Init();
        }
    }

    /// <summary>
    /// Удаление тега из контейнера и запрос на удаление
    /// </summary>
    /// <param name="tagId"></param>
    public void DelTag(string tagId)
    {
        HTTPRequests.Instance.DelTagRequest(tagId, userId);

        for (int i = 0; i < Tags.Count; i++)
        {
            if (Tags[i].id.Equals(tagId))
            {
                Tags.Remove(Tags[i]);
            }
        }
    }

    private void RemoveAllChild()
    {
        int childs = parentForTags.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(parentForTags.GetChild(i).gameObject);
        }
    }
}
