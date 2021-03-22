using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteAllTasks : MonoBehaviour
{
    [SerializeField]
    private TasksContainer tasks;

    public void CompleteAllTasks_()
    {
        for (int i = tasks.Tasks.Count - 1; i >= 0; i--)
        {
            tasks.Tasks[i].isDone = true;
            HTTPRequests.Instance.PutTaskRequest(tasks.Tasks[i]);
        }
        tasks.CreateTasksOnUI();
    }
}
