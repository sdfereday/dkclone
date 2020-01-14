using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NavAgentTest : MonoBehaviour
{
    private TaskQueue queue;
    private NavMeshAgent agent;

    private Task currentTask;
    private bool busy = false;

    private void Start()
    {
        queue = FindObjectOfType<TaskQueue>(); // Not performant
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!agent.pathPending && !busy)
        {
            if (agent.remainingDistance <= agent.stoppingDistance && currentTask != null)
            {
                Debug.Log(currentTask.target);
                transform.LookAt(currentTask.target.transform);

                StartCoroutine(SimulateTaskWait(currentTask));
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    currentTask = queue.GetFirstTask();

                    if (currentTask == null) return;

                    agent.SetDestination(currentTask.target.transform.position);
                }
            }
        }
    }

    private IEnumerator SimulateTaskWait(Task task)
    {
        busy = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        busy = false;

        Debug.Log("Obtained:" + task.resourceType);

        currentTask = null;
        queue.DestroyFirstTask();

        agent.ResetPath();
    }
}
