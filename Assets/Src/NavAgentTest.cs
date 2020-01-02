using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NavAgentTest : MonoBehaviour
{
    private TaskQueue queue;
    private NavMeshAgent agent;
    private bool committedToPause = false;

    private Task currentTask;

    private void Start()
    {
        queue = FindObjectOfType<TaskQueue>(); // Not performant
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!agent.pathPending && !committedToPause)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    currentTask = queue.GetFirstTask();

                    if (currentTask == null) return;

                    committedToPause = true;

                    agent.SetDestination(currentTask.target.transform.position);
                    //StartCoroutine("Pause");
                }
            }
        }
    }

    private IEnumerator Pause()
    {
        committedToPause = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        agent.SetDestination(agent.RandomPosition(Random.Range(10f, 20f)));
        committedToPause = false;
    }
}
