using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

// TODO: A lot of these can be popped in to states. Something to do later.
public class NavAgentTest : MonoBehaviour
{
    private TaskQueue queue;
    private NavMeshAgent agent;

    private Task currentTask;
    private bool busy = false;
    private List<System.Guid> invalidTasks = new List<System.Guid>();

    private void Start()
    {
        queue = FindObjectOfType<TaskQueue>(); // Not performant
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!agent.pathPending && !busy)
        {
            // Has arrived at task
            if (agent.remainingDistance <= agent.stoppingDistance && agent.hasPath)
            {
                if (currentTask != null)
                {
                    Debug.Log(currentTask.id);
                    Debug.Log(currentTask.target);
                    transform.LookAt(currentTask.target.transform);

                    // Double check the task hasn't become invalid since (or removed).
                    // Bear in mind, this could happen at any point so to avoid pointless
                    // running, perhaps do a lazy task check pulse thing instead.
                    if (queue.GetTaskById(currentTask.id) != null)
                    {
                        StartCoroutine(SimulateTaskWait(currentTask));
                    }
                    else
                    {
                        currentTask = null;
                    }
                } else
                {
                    // Must be wandering...
                    StartCoroutine("JustIdle");
                }                
            }

            // If wandering and a new task arrives (this would be better on a lazy check as mentioned elsewhere)
            /* Broken
            if(busy && currentTask == null)
            {
                StopCoroutine("JustIdle");
                busy = false;
            }*/

            // BUG: When selecting things in quick succession, the agent gives up and stops all of it's movement.

            // Looks for new task (might be better as a state)
            //if (agent.remainingDistance <= agent.stoppingDistance)
            //{
                if (!agent.hasPath) // || agent.velocity.sqrMagnitude == 0f
                {
                    var taskQuery = queue.GetNextTaskExcluding(invalidTasks);

                    if (taskQuery == null)
                    {
                        var randomPos = NavMeshExtensions.GetRandomPoint(transform.position, 4f);
                        agent.SetDestination(randomPos);
                        return;
                    }

                    // What sort of tile is it
                    if (taskQuery.tileType == TILE_TYPE.DIGGABLE)
                    {
                        var digNodes = taskQuery.target.GetComponent<Wall>()
                            .digNodes;

                        NavMeshPath path = new NavMeshPath();

                        foreach (Transform digNode in digNodes)
                        {
                            var calc = agent.CalculatePath(digNode.transform.position, path);

                            if (path.status == NavMeshPathStatus.PathComplete)
                            {
                                currentTask = taskQuery;
                                agent.SetDestination(digNode.transform.position);
                                break;
                            }
                        }

                        if (currentTask == null)
                        {
                            invalidTasks.Add(taskQuery.id);
                        }
                    }

                }
            //}
        }
    }

    private IEnumerator SimulateTaskWait(Task task)
    {
        busy = true;
        // TODO: Should use tile health here.
        yield return new WaitForSeconds(Random.Range(.5f, 1f));
        busy = false;

        Debug.Log("Obtained:" + task.resourceType);
        queue.DestroyTaskById(currentTask.id);

        currentTask = null;
        agent.ResetPath();

        // Temporary: A horrible quick and dirty way to force a re-check of
        // previous tasks.
        invalidTasks.Clear();
    }

    private IEnumerator JustIdle()
    {
        busy = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        busy = false;
    }
}
