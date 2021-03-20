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
}
