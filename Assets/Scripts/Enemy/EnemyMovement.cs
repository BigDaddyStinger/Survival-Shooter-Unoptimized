using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private static Transform playerT;
    private NavMeshAgent agent;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

        if (playerT == null)
        {
            var pm = Object.FindAnyObjectByType<PlayerMovement>();
            if (pm != null) playerT = pm.transform;
        }
        if (playerT != null) playerHealth = playerT.GetComponent<PlayerHealth>();
    }

    void OnEnable()
    {
        if (agent != null) agent.enabled = true;
    }

    void Update()
    {
        if (agent == null || playerT == null || enemyHealth == null || playerHealth == null)
            return;

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            agent.SetDestination(playerT.position);
        }
        else if (agent.enabled)
        {
            agent.enabled = false;
        }
    }
}