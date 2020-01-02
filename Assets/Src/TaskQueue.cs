using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public GameObject target;
    public bool taskTaken;
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
            target = taskTarget
        };

        tasks.Add(t);
    }

    public Task GetFirstTask()
    {
        return tasks.Count > 0 ? tasks[0] : null;
    }
}
