using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 500f;
    public float patrolRange = 50f;
    public float baseSpeed = 35f;
    public float speedIncreasePerLevel = 15f;
    public Vector3[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPatrolPoint = 0;
    private bool isChasing = false;
    private int difficultyLevel = 30;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Patrol());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            agent.SetDestination(player.position);
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {           
            agent.speed = baseSpeed * chaseRange * Time.deltaTime;
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!isChasing && patrolPoints.Length > 0)
            {
                agent.SetDestination(patrolPoints[currentPatrolPoint]);
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public void IncreaseDifficulty()
    {
        difficultyLevel++;
    }
}
