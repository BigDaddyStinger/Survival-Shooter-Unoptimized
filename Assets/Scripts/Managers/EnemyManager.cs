using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public string enemyPoolKey = "Enemy";
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start() => InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f) return;
        int i = Random.Range(0, spawnPoints.Length);
        var sp = spawnPoints[i];
        PoolManager.I.Spawn(enemyPoolKey, sp.position, sp.rotation);
    }
}
