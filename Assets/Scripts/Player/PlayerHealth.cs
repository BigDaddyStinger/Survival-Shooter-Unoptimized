using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public AudioClip deathClip;
    public float flashSpeed = 5f;

    public IntPairEvent onHealthChanged;
    public UnityEngine.Events.UnityEvent onDeath;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
        onHealthChanged?.Invoke(currentHealth, startingHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth = Mathf.Max(0, currentHealth - amount);
        onHealthChanged?.Invoke(currentHealth, startingHealth);
        playerAudio.Play();

        if (currentHealth <= 0 && !isDead) Death();
    }

    void Death()
    {
        isDead = true;
        playerAudio.clip = deathClip;
        playerAudio.Play();
        onDeath?.Invoke();
        if (playerMovement) playerMovement.enabled = false;
        if (playerShooting) playerShooting.enabled = false;
    }

    public void RestartLevel() => SceneManager.LoadScene(0);
}