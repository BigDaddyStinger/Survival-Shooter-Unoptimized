using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealth : MonoBehaviour
{
    public EnemySharedData shared;
    public string poolKey;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    bool isDead, isSinking;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    void Awake()
    {
        currentHealth = shared.startingHealth;
        enemyAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        var rend = GetComponentInChildren<Renderer>();
        if (rend && shared.skin) rend.sharedMaterial = shared.skin;
    }

    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.Play ();
    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //ScoreManager.AddScore(scoreValue);
        Invoke(nameof(Despawn), 2f);
    }

    private void Despawn()
    {
        if (PoolManager.I != null && !string.IsNullOrEmpty(poolKey))
        {
            PoolManager.I.Despawn(poolKey, gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
