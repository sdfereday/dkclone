using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Task
{
    public System.Guid id;
    public GameObject target;
    public bool taskTaken;
    public RESOURCE_TYPE resourceType;
    public TILE_TYPE tileType;
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
        var inboundTileData = taskTarget.GetComponent<Tile>();
        
        if (!inboundTileData.selected || tasks.Any(x => x.id == inboundTileData.Id))
        {
            // Immutable way would be better perhaps
            tasks.RemoveAll(x => x.id == inboundTileData.Id);
            return;
        }

        var t = new Task() {
            id = inboundTileData.Id, // Interface?
            target = taskTarget,
            resourceType = RESOURCE_TYPE.DIRT, // Obvs just to test it
            tileType = TILE_TYPE.DIGGABLE
        };

        tasks.Add(t);
    }

    public Task GetFirstTask()
    {
        return tasks.Count > 0 ? tasks[0] : null;
    }

    public Task GetTaskById(System.Guid Id)
    {
        return tasks.Count > 0 ? tasks
            .Where(x => x.id == Id)
            .FirstOrDefault() : null;
    }

    public Task GetNextTaskExcluding(List<System.Guid> Ids)
    {
        if (Ids.Count == 0)
            return GetFirstTask();

        return tasks.Count > 0 ? tasks
            .Where(x => Ids.All(id => id != x.id))
            .ToArray()
            .FirstOrDefault() : null;
    }

    public void DestroyFirstTask()
    {
        if (tasks.Count > 0)
        {
            Destroy(GetFirstTask().target);
            tasks.RemoveAt(0);
        }
    }

    public void DestroyTaskById(System.Guid Id)
    {
        if (tasks.Count > 0)
        {
            var task = GetTaskById(Id);
            var tIndex = tasks.FindIndex(x => x.id == Id);

            Debug.Log("Remove task at index: " + tIndex + ".");

            Destroy(task.target);
            tasks.RemoveAt(tIndex);
        }
    }
}
