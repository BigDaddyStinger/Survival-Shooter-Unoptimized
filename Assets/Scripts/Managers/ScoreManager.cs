using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score { get; private set; }
    public static UnityEvent<int> OnScoreChanged = new UnityEvent<int>();

    void Awake() { score = 0; OnScoreChanged.Invoke(score); }

    public static void AddScore(int delta)
    {
        score += delta;
        OnScoreChanged.Invoke(score);
    }
}