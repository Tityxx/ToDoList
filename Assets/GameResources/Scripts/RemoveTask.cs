using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTask : MonoBehaviour
{
    [SerializeField]
    private TaskWrapper taskWrapper;
    [SerializeField]
    private TasksContainer tasksContainer;

    /// <summary>
    /// Удаление задачи
    /// </summary>
    public void DelTask()
    {
        tasksContainer.DelTask(taskWrapper.Task.id);

        Destroy(gameObject);
    }
}
