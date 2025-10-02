using UnityEngine;

[CreateAssetMenu(fileName = "EnemySharedData", menuName = "Scriptable Objects/EnemySharedData")]
public class EnemySharedData : ScriptableObject
{
    public int startingHealth = 100;
    public float moveSpeed = 3.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public Material skin;
}