using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RemoveTag : MonoBehaviour
{
    [SerializeField]
    private TagWrapper tagWrapper;
    [SerializeField]
    private TagsContainer tagsContainer;

    /// <summary>
    /// Удаление тега
    /// </summary>
    public void DelTask()
    {
        tagsContainer.DelTag(tagWrapper.Tag.id);

        Destroy(gameObject);
    }
}
