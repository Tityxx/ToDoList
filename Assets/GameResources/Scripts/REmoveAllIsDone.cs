using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REmoveAllIsDone : MonoBehaviour
{
    [SerializeField]
    private TasksContainer tasks;

    public void RemoveAllIsDoneTasks()
    {
        for (int i = tasks.Tasks.Count - 1; i >= 0; i--)
        {
            if (tasks.Tasks[i].isDone)
            {
                HTTPRequests.Instance.DelTaskRequest(tasks.Tasks[i].id, tasks.userId);
                tasks.Tasks.RemoveAt(i);
            }
        }

        tasks.CreateTasksOnUI();
    }
}
