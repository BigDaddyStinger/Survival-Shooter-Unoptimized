using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreTextUI : MonoBehaviour
{
    public Text text;

    void OnEnable()
    {
        ScoreManager.OnScoreChanged.AddListener(UpdateText);
        UpdateText(ScoreManager.score);
    }
    void OnDisable()
    {
        ScoreManager.OnScoreChanged.RemoveListener(UpdateText);
    }
    void UpdateText(int s)
    {
        if (text) text.text = "Score: " + s;
    }
}