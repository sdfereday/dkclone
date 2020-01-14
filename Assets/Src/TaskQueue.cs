using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public GameObject target;
    public bool taskTaken;
    public RESOURCE_TYPE resourceType;
}

public class TaskQueue : MonoBehaviour
{
    private List<Task> tasks;

    private void Awake()
    {
        tasks = new List<Task>();
    }

    public void AddTask(GameObject taskTarget)
    {
        var t = new Task() {
            target = taskTarget,
            resourceType = RESOURCE_TYPE.WATER // Obvs just to test it
        };

        tasks.Add(t);
    }

    public Task GetFirstTask()
    {
        return tasks.Count > 0 ? tasks[0] : null;
    }

    public void DestroyFirstTask()
    {
        if (tasks.Count > 0)
        {
            Destroy(GetFirstTask().target);
            tasks.RemoveAt(0);
        }
    }
}
