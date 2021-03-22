using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Обёртка для задач
/// </summary>
public class TaskWrapper : MonoBehaviour
{
    public Task Task;
    public Text taskTextField;

    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Text taskName;
    [SerializeField]
    private Text taskDateStart;
    [SerializeField]
    private Text taskDateEnd;
    [SerializeField]
    private GameObject tagPrefab;
    [SerializeField]
    private Transform parentForTags;
    [SerializeField]
    private TasksContainer tasksContainer;

    private void Start()
    {
        toggle.onValueChanged.AddListener(SetTaskInfo);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(SetTaskInfo);
    }

    /// <summary>
    /// Установить текстовое поле с инофрмацией 
    /// о задаче при нажатии на неё
    /// </summary>
    /// <param name="isOn"></param>
    public void SetTaskInfo(bool isOn)
    {
        taskTextField.text = isOn ? Task.info : string.Empty;
    }

    public void Init()
    {
        taskName.text = Task.name;
        taskDateStart.text = Task.dateStart.Equals("") ? Task.dateStart : DateTime.Parse(Task.dateStart).ToString();
        taskDateEnd.text = Task.dateEnd.Equals("") ? Task.dateEnd : DateTime.Parse(Task.dateEnd).ToString();

        foreach (string tag in Task.tags)
        {
            GameObject goTag = Instantiate(tagPrefab, parentForTags);
            TagWrapper tagWrapper = goTag.GetComponent<TagWrapper>();
            tagWrapper.Tag = new Tag(string.Empty, tag, Task.userId);
            tagWrapper.Init();
        }
    }

    public void SetTaskId()
    {
        tasksContainer.LastUpdatedTaskId = Task.id;
    }
}
